using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTOs;
using RWA.BL.BLModels;
using RWA.BL.DALModels;
using RWA.BL.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepo _projectRepo;

        public ProjectController(IMapper mapper, IProjectRepo projectRepo)
        {
            _mapper = mapper;
            _projectRepo = projectRepo;
        }

        // GET: api/<ProjectController>
        [HttpGet]
        public ActionResult<IEnumerable<ProjectDto>> Get()
        {
            try
            {
                var result = _projectRepo.GetAll();
                //var result = _context.Projects.Include().Include("SkillSet");
                var resultDto = _mapper.Map<IEnumerable<ProjectDto>>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public ActionResult<BlProject> Get(int id)
        {
            try
            {
                var project = _projectRepo.Get(id);
                if (project == null)
                {
                    return NotFound();
                }
                ProjectDto projectDto = _mapper.Map<ProjectDto>(project);
                return Ok(projectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("[action]")]
        public ActionResult<IEnumerable<ProjectDto>> Search(string searchPart)
        {
            try
            {
                var dbProjects = _projectRepo.SearchByTitle(searchPart, 1, _projectRepo.GetAll().Count(), null);

                if (dbProjects.Count() == 0)
                {
                    return NotFound();
                }

                var projects = _mapper.Map<IEnumerable<ProjectDto>>(dbProjects);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProjectController>
        [HttpPost]
        public ActionResult<ProjectDto> Post([FromBody] ProjectDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.SkillSets.Count() == 0)
                {
                    return BadRequest("At least 1 skill set is required");
                }

                var newProject = _projectRepo.Add(_mapper.Map<BlProject>(value));

                value.Idproject = newProject.Idproject;

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith(@"The INSERT statement conflicted with the FOREIGN KEY constraint") && ex.InnerException.Message.Contains("FK__Project__TypeID__47DBAE45"))
                {
                    return BadRequest("Invalid project type id");
                }
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith(@"The INSERT statement conflicted with the FOREIGN KEY constraint") && ex.InnerException.Message.Contains("FK__ProjectSk__Skill__4BAC3F29"))
                {
                    return BadRequest("Invalid skill set id");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public ActionResult<ProjectDto> Put(int id, [FromBody] ProjectDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                value.Idproject = id;

                var result = _projectRepo.Update(_mapper.Map<BlProject>(value));

                if (result == null)
                {
                    return NotFound();
                }

                value.Idproject = result.Idproject;

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith(@"The INSERT statement conflicted with the FOREIGN KEY constraint") && ex.InnerException.Message.Contains("FK__Project__TypeID__47DBAE45"))
                {
                    return BadRequest("Invalid project type id");
                }
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith(@"The INSERT statement conflicted with the FOREIGN KEY constraint") && ex.InnerException.Message.Contains("FK__ProjectSk__Skill__4BAC3F29"))
                {
                    return BadRequest("Invalid skill set id");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public ActionResult<ProjectDto> Delete(int id)
        {
            try
            {
                var result = _projectRepo.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                ProjectDto resultDto = _mapper.Map<ProjectDto>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
