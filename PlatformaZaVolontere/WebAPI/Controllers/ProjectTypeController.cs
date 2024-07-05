using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTOs;
using RWA.BL.BLModels;
using RWA.BL.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectTypeRepo _projectTypeRepo;
        public ProjectTypeController(IMapper mapper, IProjectTypeRepo projectTypeRepo)
        {
            _mapper = mapper;
            _projectTypeRepo = projectTypeRepo;
        }

        // GET: api/<ProjectTypesController>
        [HttpGet]
        public ActionResult<IEnumerable<ProjectTypeDto>> Get()
        {
            try
            {
                var result = _projectTypeRepo.GetAll();
                var resultDto = _mapper.Map<IEnumerable<ProjectTypeDto>>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ProjectTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ProjectTypeDto> Get(int id)
        {
            try
            {
                var projectType = _projectTypeRepo.Get(id);
                if (projectType == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProjectTypeDto>(projectType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProjectTypesController>
        [HttpPost]
        public ActionResult<ProjectTypeDto> Post([FromBody] ProjectTypeDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                value.IdprojectType = 0;
                var newType = _projectTypeRepo.Add(_mapper.Map<BlProjectType>(value));
                
                value.IdprojectType = newType.IdprojectType;

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    return BadRequest("That project type already exists");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProjectTypesController>/5
        [HttpPut("{id}")]
        public ActionResult<ProjectTypeDto> Put(int id, [FromBody] ProjectTypeDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                value.IdprojectType = id;

                if (_projectTypeRepo.Update(_mapper.Map<BlProjectType>(value)) == null)
                {
                    return NotFound();
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    return BadRequest("That project type already exists");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProjectTypesController>/5
        [HttpDelete("{id}")]
        public ActionResult<ProjectTypeDto> Delete(int id)
        {
            try
            {
                var result = _projectTypeRepo.Delete(id);

                if (result == null)
                {
                    return NotFound();
                }

                var resultDto = _mapper.Map<ProjectTypeDto>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}