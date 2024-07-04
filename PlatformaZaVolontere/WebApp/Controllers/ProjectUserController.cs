using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWA.BL.Repositories;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ProjectUserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectUserRepo _projectUserRepo;
        public ProjectUserController(IMapper mapper, IProjectUserRepo projectUserRepo)
        {
            _mapper = mapper;
            _projectUserRepo = projectUserRepo;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<ProjectUserVM>>(_projectUserRepo.GetAll()));
        }
    }
}
