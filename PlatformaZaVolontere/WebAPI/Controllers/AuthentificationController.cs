using AutoMapper;
using Bl.Utilities;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTOs;
using RWA.BL.BLModels;
using RWA.BL.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepo;

        public AuthentificationController(IUserRepo userRepo, IMapper mapper, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public ActionResult<UserDto> Register([FromBody] UserRegistrationDto registerData){
            try
            {
                return Ok(_mapper.Map<UserDto>(_userRepo.Add(_mapper.Map<BlUser>(registerData))));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("[action]")]
        public ActionResult<string> Login([FromBody] UserLoginDto loginData)
        {
            try
            {
                BlUser? user = _userRepo.Login(_mapper.Map<BlUser>(loginData));
                if (user == null)
                {
                    return BadRequest("Bad username or password");
                }

                var secureKey = _configuration["JWT:SecureKey"];

                var serializedToken =
                    JwtTokenProvider.CreateToken(
                        secureKey,
                        120,
                        user.Username,
                        user.Role.Name);

                return Ok(serializedToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult UserChangePassword(UserChangePasswordDto changePasswordDto)
        {
            try
            {
                if (!_userRepo.ChangePassword(changePasswordDto.Username, changePasswordDto.OldPassword, changePasswordDto.NewPassword))
                {
                    return NotFound();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /*[HttpPost("[action]")]
        public ActionResult AdminChangePassword(AdminChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Username.Equals(changePasswordDto.Username.Trim()));
                if (user == null)
                {
                    return BadRequest($"No such user as {changePasswordDto.Username.Trim()}");
                }

                user.PasswordSalt = AuthUtilities.GetSalt();
                user.PasswordHash = AuthUtilities.GetStringSha256Hash(changePasswordDto.Password, user.PasswordSalt);

                // Save changes to database
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/
    }
}