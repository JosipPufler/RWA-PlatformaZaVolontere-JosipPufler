using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWA.BL.BLModels;
using RWA.BL.Repositories;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ProjectTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectTypeRepo _projectTypeRepo;

        public ProjectTypeController(IMapper mapper, IProjectTypeRepo projectTypeRepo)
        {
            _mapper = mapper;
            _projectTypeRepo = projectTypeRepo;
        }

        [Authorize(Roles = "Admin")]
        // GET: ProjectTypeController
        public ActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<ProjectTypeVM>>(_projectTypeRepo.GetAll()));
        }

        [Authorize(Roles = "Admin")]
        // GET: ProjectTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: ProjectTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: ProjectTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectTypeVM projectType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to create project type");

                    return View();
                }
                _projectTypeRepo.Add(_mapper.Map<BlProjectType>(projectType));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(ProjectTypeVM.Name), "That project type already exists");
                }
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: ProjectTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_mapper.Map<ProjectTypeVM>(_projectTypeRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        // POST: ProjectTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectTypeVM projectType)
        {
            try
            {
                projectType.IdprojectType = id;
                _projectTypeRepo.Update(_mapper.Map<BlProjectType>(projectType));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(SkillSetVM.Name), "That project type already exists");
                }
                return View(_mapper.Map<ProjectTypeVM>(_projectTypeRepo.Get(id)));
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: ProjectTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mapper.Map<ProjectTypeVM>(_projectTypeRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        // POST: ProjectTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _projectTypeRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
