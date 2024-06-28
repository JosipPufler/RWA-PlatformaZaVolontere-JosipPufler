/*using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTOs;
using RestApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSkillSetController : ControllerBase
    {
        private readonly RwaContext _context;
        private readonly IMapper _mapper;

        public ProjectSkillSetController(RwaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ProjectSkillSetController>
        [HttpGet]
        public ActionResult<IEnumerable<ProjectSkillSetDto>> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<ProjectSkillSetDto>>(_context.ProjectSkillSets));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ProjectSkillSetController>/5
        [HttpGet("{id}")]
        public ActionResult<ProjectSkillSetDto> Get(int id)
        {
            try
            {
                var projectSkillSet = _context.ProjectSkillSets.FirstOrDefault(x => x.Id == id);
                if (projectSkillSet == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProjectSkillSetDto>(projectSkillSet));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProjectSkillSetController>
        [HttpPost]
        public ActionResult<ProjectSkillSetDto> Post([FromBody] ProjectSkillSetDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newProjectSkillSet = _mapper.Map<ProjectSkillSet>(value);

                _context.ProjectSkillSets.Add(newProjectSkillSet);
                _context.SaveChanges();

                value.Id = newProjectSkillSet.Id;

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProjectSkillSetController>/5
        [HttpPut("{id}")]
        public ActionResult<ProjectSkillSetDto> Put(int id, [FromBody] ProjectSkillSetDto value)
        {
            try
            {
                if (id != value.Id)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var projectSkillSetToUpdate = _context.ProjectSkillSets.FirstOrDefault(x => x.Id == id);

                if (projectSkillSetToUpdate == null)
                {
                    return NotFound();
                }

                _context.ProjectSkillSets.Update(projectSkillSetToUpdate);
                _context.SaveChanges();

                value.Id = projectSkillSetToUpdate.Id;

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProjectSkillSetController>/5
        [HttpDelete("{id}")]
        public ActionResult<ProjectSkillSetDto> Delete(int id)
        {
            try
            {
                var projectSkillSet = _context.ProjectSkillSets.FirstOrDefault(x => x.Id == id);

                if (projectSkillSet == null)
                {
                    return NotFound();
                }
                var projectSkillSetDto = _mapper.Map<ProjectSkillSetDto>(projectSkillSet);
                _context.ProjectSkillSets.Remove(projectSkillSet);
                _context.SaveChanges();

                return Ok(projectSkillSetDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
*/