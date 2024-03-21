using JobPortalAPI_1.Repository;
using JobPortalAPI_1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRetrivingData _retrivingData;
        public AdminController(IRetrivingData retrivingData)
        {
                _retrivingData = retrivingData;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLists()
        {
            var userList=await _retrivingData.GetUsers();
            if (userList != null) 
            {
                return Ok(userList);
            }
            return BadRequest("Error in Retriving Datas");
        }

    }
}
