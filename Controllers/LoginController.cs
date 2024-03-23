using JobPortalAPI_1.Services;
using JobPortalAPI_1.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginHandling _loginhandling;
        public LoginController(ILoginHandling loginHandling)
        {
            _loginhandling = loginHandling;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateUserLogin([FromBody]LoginCredintials credintials)
        {
            try
            {

                var token = await _loginhandling.UserLoginHandler(credintials);
                if (token != null)
                {
                    return Ok(token);
                }
                return Unauthorized("Invalid Credentials");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Message:{ex.Message}");
            }
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> ValidateAdminLogin([FromBody]LoginCredintials credintials) 
        {
            try
            {
              var token=await  _loginhandling.AdminLoginHandler(credintials);
                if (token!=null)
                {
                    return Ok(token);
                }
                return Unauthorized("Invalid Credentials");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error Message:{ex.Message}");
            }
        }
    }
}
