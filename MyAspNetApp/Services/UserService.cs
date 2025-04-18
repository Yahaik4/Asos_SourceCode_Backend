using Microsoft.Extensions.Configuration;
using MyAspNetApp.DTOs.User;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;
using System.Security.Cryptography;
using System.Text;
using MyAspNetApp.Utils.Template;

namespace MyAspNetApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Auth _authService;
        private readonly Mailer _mailer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, Auth auth, Mailer mailer, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _authService  = auth;
            _mailer = mailer;
            _httpContextAccessor = httpContextAccessor;
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
        private string GenerateOTP(int length = 6)
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, length).Select(_ => random.Next(0, 10)));
        }

        public async Task<string> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {           
            try{
                var emailExisted = await _userRepository.FindUserByEmail(userResetPasswordDto.Email);
                if(emailExisted == null){
                    throw new Exception("Email Không tồn tại");
                }

                string otp = GenerateOTP();
                string htmlContent = OTPTemplate.Generate(otp);
                
                await _mailer.SendMail(userResetPasswordDto.Email, "Your OTP", htmlContent);

                _httpContextAccessor.HttpContext.Session.SetString("OTP", otp);
                _httpContextAccessor.HttpContext.Session.SetString("OTP_Email", userResetPasswordDto.Email);

                
                return "Send Mail success";
            }catch(Exception error){
                throw new Exception("Email không tồn tại", error);
            }
        }

        public async Task<string> VevifyOTP(UserVerifyOTPDto userVerifyOTPDto)
        {
            var savedOtp = _httpContextAccessor.HttpContext.Session.GetString("OTP");
            var savedEmail = _httpContextAccessor.HttpContext.Session.GetString("OTP_Email");

            if (string.IsNullOrEmpty(savedOtp) || string.IsNullOrEmpty(savedEmail))
            {
                throw new Exception("OTP has expired or not found.");
            }

            if (savedOtp == userVerifyOTPDto.otp && savedEmail == userVerifyOTPDto.Email)
            {
                string newPassword = await _userRepository.ChangePassword(savedEmail);
                string htmlContent = OTPTemplate.Generate(newPassword);

                await _mailer.SendMail(savedEmail, "Your PassWord", htmlContent);

                return "OTP verified successfully.";
            }

            throw new Exception("OTP is incorrect or does not match email.");
        }

        public async Task<int> GetUserId(HttpContext httpContext){
            if(!httpContext.Request.Cookies.ContainsKey("token")){
                throw new Exception("Token khong ton tai");
            };
            var userId = Auth.GetUserIdFromToken(httpContext);
            return int.Parse(userId);
        }



    }
}
