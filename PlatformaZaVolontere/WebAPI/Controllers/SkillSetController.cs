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
    public class SkillSetController : ControllerBase
    {
        private readonly ISkillSetRepo _skillSetRepo;
        private readonly IMapper _mapper;

        public SkillSetController(IMapper mapper, ISkillSetRepo skillSetRepo)
        {
            _mapper = mapper;
            _skillSetRepo = skillSetRepo;
        }

        // GET: api/<SkillSetController>
        [HttpGet]
        public ActionResult<IEnumerable<SkillSetDto>> Get()
        {
            try
            {
                var result = _skillSetRepo.GetAll();
                
                return Ok(_mapper.Map<IEnumerable<SkillSetDto>>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<SkillSetController>/5
        [HttpGet("{id}")]
        public ActionResult<SkillSetDto> Get(int id)
        {
            try
            {
                var skillSet = _skillSetRepo.Get(id);

                if (skillSet == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<SkillSetDto>(skillSet));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<SkillSetController>
        [HttpPost]
        public ActionResult<SkillSetDto> Post([FromBody] SkillSetDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newSkill = _skillSetRepo.Add(_mapper.Map<BlSkillSet>(value));
                value.IdskillSet = newSkill.IdskillSet;

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<SkillSetController>/5
        [HttpPut("{id}")]
        public ActionResult<SkillSetDto> Put(int id, [FromBody] SkillSetDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                value.IdskillSet = id;
                var result = _skillSetRepo.Update(_mapper.Map<BlSkillSet>(value));

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<SkillSetController>/5
        [HttpDelete("{id}")]
        public ActionResult<SkillSetDto> Delete(int id)
        {
            try
            {
                var result = _skillSetRepo.Delete(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<SkillSetDto>(result));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}