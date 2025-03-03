using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // API: GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUser();
            return Ok(new
            {
                statusCode = 200,
                msg = "Get All User Success",
                metadata = users,
            });
        }


        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            try{
                var updatedUser = await _userService.UpdateUser(user);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "User updated successfully",
                    metadata = updatedUser
                });
            }
            catch(Exception error){
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message,
                });
            }
        }
    }
}
