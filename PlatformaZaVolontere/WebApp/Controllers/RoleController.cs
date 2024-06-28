using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWA.BL.BLModels;
using RWA.BL.Repositories;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepo roleRepo, IMapper mapper)
        {
                _roleRepo = roleRepo;
                _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var roles = _mapper.Map<IEnumerable<RoleVM>>(_roleRepo.GetAll());
            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(RoleVM role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to create role");

                    return View();
                }

                _roleRepo.Add(_mapper.Map<BlRole>(role));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var role = _mapper.Map<RoleVM>(_roleRepo.Get(id));
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, RoleVM role)
        {
            try
            {
                role.Idrole = id;
                _roleRepo.Update(_mapper.Map<BlRole>(role));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var role = _mapper.Map<RoleVM>(_roleRepo.Get(id));
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, RoleVM role)
        {
            try
            {
                _roleRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
