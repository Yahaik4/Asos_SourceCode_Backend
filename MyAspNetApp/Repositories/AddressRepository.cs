using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAddress(Address Address)
        {
            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();
            return Address;
        }

        public async Task<bool> DeleteAddress(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Address> GetAddressById(int Id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(address => address.Id == Id);
        }

        public async Task<IEnumerable<Address>> GetAddressByUserId(int Id)
        {
            return await _context.Addresses.
            Where(Address => Address.UserId == Id).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddress()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> UpdateAddress(Address Address)
        {
            var existed = await GetAddressById(Address.Id);

            if(existed == null){
                throw new Exception("Address not existed");
            }

            existed.Street = Address.Street;
            existed.City = Address.City;
            existed.Country = Address.Country;
            existed.PostalCode = existed.PostalCode;

            _context.Addresses.Update(existed);

            await _context.SaveChangesAsync();

            return existed;
        }
    }
}