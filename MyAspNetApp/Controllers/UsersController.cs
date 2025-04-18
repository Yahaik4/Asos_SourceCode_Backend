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
        private readonly IUserRepository _userRepo;

        public UsersController(IUserService userService, IUserRepository userRepo)
        {
            _userService = userService;
            _userRepo = userRepo;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
        {
            var users = await _userRepo.FindUserById(id);
            return Ok(new
            {
                statusCode = 200,
                msg = "Get User Success",
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
