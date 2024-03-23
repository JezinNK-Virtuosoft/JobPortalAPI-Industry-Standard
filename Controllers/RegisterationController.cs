using JobPortalAPI_1.Repository;
using JobPortalAPI_1.Services;
using JobPortalAPI_1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI_1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {
        private readonly IRegistration _registration;
        private readonly IValidation _validation;
        public RegisterationController(IRegistration registration, IValidation validation)
        {
            _registration = registration;
            _validation = validation;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationDetails details)
        {   
            if (details != null)
            {
                bool inserted = await _registration.Register(details);
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
        [HttpGet("{Email}")]
        public async Task<IActionResult> CheckEmailExists(string Email)
        {
            bool Exists=await _validation.EmailExists(Email);
            if (Exists)
            {
                return BadRequest("Email already Exists");
            }
            return Ok("Email does not Exists");    
        }
    }
}
