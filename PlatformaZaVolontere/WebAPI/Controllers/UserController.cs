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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        public UserController(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<UserDto>>(_userRepo.GetAll()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            try
            {
                var user = _userRepo.Get(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult<UserDto> Put(int id, [FromBody] UserDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _userRepo.Update(_mapper.Map<BlUser>(value));
                if (result == null)
                {
                    return NotFound();
                }

                value.Iduser = result.Iduser;

                return Ok(value);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith(@"The INSERT statement conflicted with the FOREIGN KEY constraint") && ex.InnerException.Message.Contains("FK__ProjectSk__Skill__4BAC3F29"))
                {
                    return BadRequest("Invalid skill set id");
                }
                if (ex.InnerException != null && ex.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    return BadRequest("That user already exists");
                }
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult<UserDto> Delete(int id)
        {
            try
            {
                var result = _userRepo.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                var resultDto = _mapper.Map<UserDto>(result);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
