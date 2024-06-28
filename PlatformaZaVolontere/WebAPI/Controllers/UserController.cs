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

        // POST api/<UserController>
        /*[HttpPost]
        public ActionResult<UserDto> Post([FromBody] UserDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var salt = AuthUtilities.GetSalt();
                value


                _context.Users.Add(newUser);
                _context.SaveChanges();

                value.Iduser = newUser.Iduser;

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/

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
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
