using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs.User;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase{

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // API: POST api/auth
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            try{

                var newUser = await _userService.CreateUser(user);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Register successfully",
                    metadata = newUser,
                });
            }catch(Exception error){
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message,
                });
            }
            
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            
            try{
                var result = await _userService.Login(userLoginDto, HttpContext);

                return Ok(new {
                    statusCode = 200,
                    msg = "Login successfully",
                    metadata = result,
                });
            }catch(Exception error){
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message,
                });
            }
        }


        [HttpPost("signout")]
        public async Task<ActionResult> SignOut(){
            try{
                var result = await _userService.SignOut(HttpContext);

                return Ok(new{
                    statusCode = 200,
                    msg = "SignOut successfully",
                    metadata = result,
                });
            }catch(Exception error){
                    return BadRequest(new
                    {
                        statusCode = 400,
                        msg = error.Message,
                    });
            }
            
        }


        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            try{
                var result = await _userService.ResetPassword(userResetPasswordDto);

                return Ok(new{
                    statusCode = 200,
                    msg = "SendMail successfully",
                    metadata = result,
                });
            }catch(Exception error){
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message,
                });
            }
        }

    }

}