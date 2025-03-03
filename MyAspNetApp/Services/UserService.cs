using Microsoft.Extensions.Configuration;
using MyAspNetApp.DTOs.User;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Repositories;
using MyAspNetApp.Utils;
using System.Security.Cryptography;
using System.Text;


namespace MyAspNetApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Auth _authService;
        private readonly Mailer _mailer;

        public UserService(IUserRepository userRepository, Auth auth, Mailer mailer)
        {
            _userRepository = userRepository;
            _authService  = auth;
            _mailer = mailer;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _userRepository.GetAllUser();
        }

        public async Task<User> CreateUser(User user)
        {
            var existed = await _userRepository.FindUserByEmail(user.Email);

            if(existed != null)
            {
                throw new Exception("Email đã tồn tại.");
            }

            user.Password = HashPassword(user.Password);
            
            return await _userRepository.CreateUser(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                var computedHash = Convert.ToBase64String(hash);

                return computedHash == hashedPassword;
            }
        }

        public async Task<User?> UpdateUser(User user)
        {
            var updatedUser = await _userRepository.UpdateUser(user) ?? throw new Exception("User không tồn tại");
            return updatedUser;
        }

        public async Task<string> Login(UserLoginDto userLoginDto, HttpContext httpContext)
        {
            var user = await _userRepository.FindUserByEmail(userLoginDto.Email) ?? throw new Exception("User không tồn tại");
            if(!VerifyPassword(userLoginDto.Password, user.Password)){
                throw new Exception("Sai mật khẩu");
            }

            _authService.generateToken(httpContext, user.Id.ToString(), user.Email, user.Role);

            if(httpContext.Request.Cookies.ContainsKey("token")){
                // Console.WriteLine(httpContext.Request.Cookies["token"]);
                return httpContext.Request.Cookies["token"];
            };

            return null;
        }

        public async Task<bool> SignOut(HttpContext httpContext)
        {
            if(httpContext.Request.Cookies.ContainsKey("token")){
                // Console.WriteLine(httpContext.Request.Cookies["token"]);
                httpContext.Response.Cookies.Delete("token");
                return true;
            };
            return false;
        }

        public async Task<string> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {           
            try{
                await _mailer.SendMail(userResetPasswordDto.Email, "Your OTP", "123456");
                return "Send Mail success";
            }catch(Exception error){
                Console.WriteLine("Huy");
                throw new Exception("Email không tồn tại", error);
                // return $"Lỗi khi gửi OTP: {error}";
            }
        }


    }
}
