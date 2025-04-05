using MyAspNetApp.DTOs.User;
using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> CreateUser(User user);
        Task<User?> UpdateUser(User user);
        Task<string> Login(UserLoginDto userLoginDto, HttpContext httpContext);
        Task<bool> SignOut(HttpContext httpContext);
        Task<string> ResetPassword(UserResetPasswordDto userResetPasswordDto);
        Task<string> VevifyOTP(UserVerifyOTPDto userVerifyOTPDto);
        Task<int> GetUserId(HttpContext httpContext);
    }
}
