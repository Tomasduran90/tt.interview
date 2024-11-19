using Microsoft.AspNetCore.Mvc;
using tt.interview.Repositories;

namespace tt.interview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TTUserController : ControllerBase
    {
        private readonly ILogger<TTUserController> _logger;
        private readonly IUserRepository _userRepository;

        public TTUserController(ILogger<TTUserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> Get([FromQuery]string userName, [FromQuery] string password)
        {

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            try
            {
                var user = await _userRepository.GetUser(userName, password);

                if (user == null)
                {
                    return NotFound("not found user");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
