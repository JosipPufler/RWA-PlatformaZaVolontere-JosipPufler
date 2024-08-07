﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RWA.BL.BLModels;
using RWA.BL.Repositories;
using System.Security.Claims;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepo _projectRepo;
        private readonly ISkillSetRepo _skillSetRepo;
        private readonly IProjectTypeRepo _projectTypeRepo;
        private readonly IConfiguration _configuration;
        public ProjectController(IMapper mapper, IProjectRepo projectRepo, ISkillSetRepo skillSetRepo, IProjectTypeRepo projectTypeRepo, IConfiguration configuration)
        {
            _mapper = mapper;
            _projectRepo = projectRepo;
            _skillSetRepo = skillSetRepo;
            _projectTypeRepo = projectTypeRepo;
            _configuration = configuration;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminSearch(SearchVM searchVm)
        {
            PrepareSearch(searchVm);

            return View(searchVm);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminSearchPartial(SearchVM searchVm)
        {
            PrepareSearch(searchVm);

            return PartialView("_AdminSearchPartial", searchVm);
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult Search(SearchVM searchVm)
        {
            try
            {
                PrepareSearch(searchVm);

                return View(searchVm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult SearchPartial(SearchVM searchVm)
        {
            try
            {
                PrepareSearch(searchVm);

                return PartialView("_SearchPartial", searchVm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrepareSearch(SearchVM searchVm)
        {
            ViewBag.ProjectTypes = _mapper.Map<IEnumerable<ProjectTypeVM>>(_projectTypeRepo.GetAll());
            ViewBag.SkillSets = _mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll());
            if (string.IsNullOrEmpty(searchVm.Q) && string.IsNullOrEmpty(searchVm.Submit))
            {
                searchVm.Q = Request.Cookies["query"];
            }

            IEnumerable<BlProject> projects = _projectRepo.GetAll();
            var filteredCount = 0;
            int.TryParse(searchVm.Filter.ToLower(), out int projectTypeId);

            if (!string.IsNullOrEmpty(searchVm.Q))
            {
                projects = _projectRepo.SearchByTitle(searchVm.Q, searchVm.Page, searchVm.Size, projectTypeId);
                filteredCount = _projectRepo.SearchByTitleCount(searchVm.Q, projectTypeId);
            }
            else if (projectTypeId != 0)
            {
                projects = _projectRepo.SearchByTitle("", searchVm.Page, searchVm.Size, projectTypeId);
                filteredCount = _projectRepo.SearchByTitleCount("", projectTypeId);
            }
            else
            {
                filteredCount = projects.Count();
                projects = projects.Skip((searchVm.Page - 1) * searchVm.Size).Take(searchVm.Size);
            }

            searchVm.Projects = _mapper.Map<IEnumerable<ProjectVM>>(projects);

            var expandPages = _configuration.GetValue<int>("Paging:ExpandPages");
            searchVm.LastPage = (int)Math.Ceiling(1.0 * filteredCount / searchVm.Size);
            searchVm.FromPager = searchVm.Page > expandPages ?
                searchVm.Page - expandPages :
                1;
            searchVm.ToPager = (searchVm.Page + expandPages) < searchVm.LastPage ?
                searchVm.Page + expandPages :
                searchVm.LastPage;

            var option = new CookieOptions { Expires = DateTime.Now.AddMinutes(15) };
            Response.Cookies.Append("query", searchVm.Q ?? "", option);
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult Details(int id)
        {
            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });

            return View(_mapper.Map<ProjectVM>(_projectRepo.Get(id)));
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult AdminDetails(int id)
        {
            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
            ViewBag.RelatedUsers = _projectRepo.GetRelatedUsers(id).Select(x => x.Username);

            return View(_mapper.Map<ProjectVM>(_projectRepo.Get(id)));
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult JoinProject(int idProject) {
            var project = _projectRepo.Get(idProject);
            return View(_mapper.Map<ProjectVM>(project));
        }

        [Authorize(Roles = "Admin, Volunteer")]
        [ValidateAntiForgeryToken]
        public ActionResult JoinProjectConfirm(int idProject)
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity.FindFirst("Id").Value.ToString(), out int idUser);
                _projectRepo.JoinProject(idUser, idProject);
                return RedirectToAction(nameof(Search));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Search));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
            ViewBag.TypeSelect = _projectTypeRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdprojectType.ToString() });
            return View() ;
        }

        [Authorize(Roles = "Admin")]
        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectVM project)
        {
            try
            {
                _projectRepo.Add(_mapper.Map<BlProject>(project));
                return RedirectToAction(nameof(AdminSearch));
            }
            catch (Exception e)
            {
                ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
                ViewBag.TypeSelect = _projectTypeRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdprojectType.ToString() });
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(ProjectVM.Title), "This project title is taken");
                }
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
            ViewBag.TypeSelect = _projectTypeRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdprojectType.ToString() });
            var test = _mapper.Map<ProjectVM>(_projectRepo.Get(id));
            return View(test);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectVM projectVM)
        {
            try
            {
                projectVM.Idproject = id;
                _projectRepo.Update(_mapper.Map<BlProject>(projectVM));
                return RedirectToAction(nameof(AdminSearch));
            }
            catch (Exception e)
            {
                ViewBag.SkillSetSelect = _skillSetRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdskillSet.ToString() });
                ViewBag.RoleSelect = _projectTypeRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.IdprojectType.ToString() });
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(ProjectVM.Title), "This project title is taken");
                }
                return View(_mapper.Map<ProjectVM>(_projectRepo.Get(id)));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ViewBag.SkillSet = _mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll());
            return View(_mapper.Map<ProjectVM>(_projectRepo.Get(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _projectRepo.Delete(id);
                return RedirectToAction(nameof(AdminSearch));
            }
            catch
            {
                ViewBag.SkillSet = _mapper.Map<IEnumerable<SkillSetVM>>(_skillSetRepo.GetAll());
                return View();
            }
        }
    }
}
