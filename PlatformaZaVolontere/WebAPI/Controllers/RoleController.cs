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
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepo _roleRepo;
        public RoleController(IMapper mapper, IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> Get()
        {
            try
            {
                var result = _roleRepo.GetAll();
                var resultDto = _mapper.Map<IEnumerable<RoleDto>>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public ActionResult<RoleDto> Get(int id)
        {
            try
            {
                var role = _roleRepo.Get(id);
                if (role == null) { 
                    return NotFound();
                }

                return Ok(_mapper.Map<RoleDto>(role));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RoleController>
        [HttpPost]
        public ActionResult<RoleDto> Post([FromBody] RoleDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                value.Idrole = 0;
                var newRole = _roleRepo.Add(_mapper.Map<BlRole>(value)); 

                value.Idrole = newRole.Idrole;

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    return StatusCode(500, "That role already exists");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public ActionResult<RoleDto> Put(int id, [FromBody] RoleDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                value.Idrole = id;
                var result = _roleRepo.Update(_mapper.Map<BlRole>(value));
                
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    return StatusCode(500, "That role already exists");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public ActionResult<RoleDto> Delete(int id)
        {
            try
            {
                var result = _roleRepo.Delete(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<RoleDto>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }       
        }
    }
}