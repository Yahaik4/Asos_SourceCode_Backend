using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MyAspNetApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> FindUserByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == Email);
        }

        public async Task<User?> FindUserById(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == Id);
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUser(User user)
        {
            var existed = await _context.Users.FindAsync(user.Id);

            if(existed == null)
            {
                return null;
            }

            existed.Name = user.Name;
            existed.Email = user.Email;
            existed.Avatar = user.Avatar;
            existed.Password = user.Password;
            existed.Role = user.Role;

            await _context.SaveChangesAsync();
            return existed;

        }

        private string GeneratePassword(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)]).ToArray());
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


        public async Task<string> ChangePassword(string email)
        {   
            var user = await FindUserByEmail(email);

            if(user == null){
                throw new Exception("User khong ton tai");
            }

            string password = GeneratePassword(); 
            user.Password = HashPassword(password);
            await UpdateUser(user);
            
            return password;
        }

    }
}
