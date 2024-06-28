using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTOs;
using RWA.BL.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public LogController(ILoggerService loggerService, IMapper mapper)
        {
            _loggerService = loggerService;
            _mapper = mapper;
        }

        [HttpGet("count")]
        public ActionResult<int> Count()
        {
            try
            {
                return Ok(_loggerService.Count());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/<LogController>
        [HttpGet("get/{N}")]
        public ActionResult<IEnumerable<LogDto>> Get(int N)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<LogDto>>(_loggerService.GetLogs(N)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
