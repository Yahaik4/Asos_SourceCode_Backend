using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;



namespace MyAspNetApp.Utils{
    public class Auth
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration){
            _configuration = configuration;
        }

        public void generateToken(HttpContext httpContext, string userId, string email, string role)
        {
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId), 
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var SECRET_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            var credentials = new SigningCredentials(SECRET_KEY, SecurityAlgorithms.HmacSha256);

            var expiresIn = DateTime.UtcNow.AddMinutes(60);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expiresIn,
                signingCredentials: credentials
            );

            var Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,  // Chỉ truy cập qua server
                Secure = true,    // Chỉ sử dụng qua HTTPS
                SameSite = SameSiteMode.Lax, // Ngăn CSRF
                Expires = expiresIn,  // Thời gian hết hạn
                Path = "/",        // Đường dẫn hợp lệ
                Domain = "localhost",  // Tên miền cookie có hiệu lực
            };

            httpContext.Response.Cookies.Append("token", Token, cookieOptions);
        }
    }
}
