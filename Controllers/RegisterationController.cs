using JobPortalAPI_1.Repository;
using JobPortalAPI_1.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {
        private readonly IRegistration _registration;
        public RegisterationController(IRegistration registration)
        {
            _registration = registration;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegistration([FromBody]UserRegistrationDetails details)
        {
            if (details != null)
            {
                bool inserted=await _registration.Register(details);
                if (inserted)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Error in Adding User");
                }
            }
            else 
            {
                return BadRequest("Enter the Neccessary Details");
            }

        }
    }
}
