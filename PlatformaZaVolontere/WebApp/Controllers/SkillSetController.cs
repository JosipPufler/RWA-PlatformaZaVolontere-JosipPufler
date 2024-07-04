using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RWA.BL.BLModels;
using RWA.BL.DALModels;
using RWA.BL.Repositories;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class SkillSetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISkillSetRepo _skillSetRepo;

        public SkillSetController(IMapper mapper, ISkillSetRepo skillSetRepo)
        {
            _mapper = mapper;
            _skillSetRepo = skillSetRepo;
        }

        [Authorize(Roles = "Admin")]
        // GET: SkillSetController
        public ActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll()));
        }

        [Authorize(Roles = "Admin")]
        // GET: SkillSetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: SkillSetController/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: SkillSetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillSetVM skillSetVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to create skill set");

                    return View();
                }
                _skillSetRepo.Add(_mapper.Map<BlSkillSet>(skillSetVM));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(SkillSetVM.Name), "That skill set already exists");
                }
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: SkillSetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_mapper.Map<SkillSetVM>(_skillSetRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        // POST: SkillSetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SkillSetVM skillSetVM)
        {
            try
            {
                skillSetVM.IdskillSet = id;
                _skillSetRepo.Update(_mapper.Map<BlSkillSet>(skillSetVM));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(SkillSetVM.Name), "That skill set already exists");
                }
                return View(_mapper.Map<SkillSetVM>(_skillSetRepo.Get(id)));
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: SkillSetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mapper.Map<SkillSetVM>(_skillSetRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        // POST: SkillSetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SkillSetVM skillSet)
        {
            try
            {
                _skillSetRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
