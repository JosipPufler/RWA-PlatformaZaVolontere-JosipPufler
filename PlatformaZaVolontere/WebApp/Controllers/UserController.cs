using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RWA.BL.BLModels;
using RWA.BL.Repositories;
using System.Security.Claims;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ISkillSetRepo _skillSetRepo;
        private readonly IRoleRepo _roleRepo;
        public UserController(IMapper mapper, IUserRepo userRepo, ISkillSetRepo skillSetRepo, IRoleRepo roleRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _skillSetRepo = skillSetRepo;
            _roleRepo = roleRepo;
        }

        [Authorize(Roles = "Admin")]
        // GET: UserController
        public ActionResult Index()
        {
            ViewBag.SkillSets = _mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll());
            return View(_mapper.Map<IEnumerable<UserVM>>(_userRepo.GetAll()));
        }

        [Authorize(Roles = "Admin")]
        // GET: UserController/Create
        public ActionResult Create()
        {
            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
            ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });

            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVM user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to create user");
                    ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
                    ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });

                    return View();
                }
                var blUser = _mapper.Map<BlUser>(user);
                _userRepo.Add(blUser);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
                ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(UserVM.Username), "That username is taken");
                }
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity.FindFirst("Id").Value.ToString(), out int idUser);
                id = idUser;
            }

            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
            ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });

            return View(_mapper.Map<UserVM>(_userRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserVM user)
        {
            try
            {
                user.Iduser = id;
                var blUser = _mapper.Map<BlUser>(user);
                _userRepo.Update(blUser);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
                ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(UserVM.Username), "That username is taken");
                }
                return View();
            }
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult Profile(int id, string returnUrl)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            int.TryParse(claimsIdentity.FindFirst("Id").Value.ToString(), out int idUser);
            id = idUser;

            ViewBag.returnUrl = returnUrl;
            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
            ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });

            return View(_mapper.Map<UserVM>(_userRepo.Get(id)));
        }

        [Authorize(Roles = "Admin, Volunteer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(int id, UserVM user, string returnUrl)
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity.FindFirst("Id").Value.ToString(), out int idUser);
                id = idUser;
                var blUser = _mapper.Map<BlUser>(user);
                blUser.Iduser = id;
                var existingUser = _userRepo.Get(id);
                blUser.Role = existingUser.Role;
                _userRepo.Update(blUser);
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
                ViewBag.RoleSelect = _roleRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Idrole.ToString() });

                return View();
            }
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public JsonResult GetProfileData(int id)
        {
            var user = _userRepo.Get(id);
            var json = Json(new
            {
                user.Username,
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber
            });
            return json;
        }

        [Authorize(Roles = "Admin, Volunteer")]
        [HttpPut]
        public ActionResult SetProfileData(int id, [FromBody] UserVM userVm)
        {
            userVm.Iduser = id;
            _userRepo.Update(_mapper.Map<BlUser>(userVm));
            
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.SkillSet = _mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll());
            return View(_mapper.Map<UserVM>(_userRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserVM user)
        {
            try
            {
                _userRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.SkillSet = _mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll());
                return View();
            }
        }
    }
}
