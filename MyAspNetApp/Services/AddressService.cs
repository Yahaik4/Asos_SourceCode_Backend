using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
        }

        public async Task<Address> CreateAddress(AddressDto addressDto, HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }
            var existingAddresses = await _addressRepository.GetAddressByUserId(int.Parse(userId));

            bool alreadyExists = existingAddresses.Any(a =>
                a.Street.Equals(addressDto.Street, StringComparison.OrdinalIgnoreCase) &&
                a.City.Equals(addressDto.City, StringComparison.OrdinalIgnoreCase) &&
                a.Country.Equals(addressDto.Country, StringComparison.OrdinalIgnoreCase) &&
                a.PostalCode.Equals(addressDto.PostalCode, StringComparison.OrdinalIgnoreCase)
            );

            if (alreadyExists)
            {
                throw new Exception("Address already exists.");
            }
            var address = new Address
            {
                UserId = int.Parse(userId),
                Street = addressDto.Street,
                City = addressDto.City,
                Country = addressDto.Country,
                PostalCode = addressDto.PostalCode
            };

            return await _addressRepository.CreateAddress(address);
        }

        public async Task<bool> DeleteAddress(int Id)
        {
            var address = await _addressRepository.GetAddressById(Id);

            if(address != null)
            {
                await _addressRepository.DeleteAddress(address);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Address>> GetAllAddress(HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }
            var addresses = await _addressRepository.GetAddressByUserId(int.Parse(userId));
            if(addresses == null){
                return null;
            }

            return addresses;

        }

        public async Task<Address> UpdateAddress(Address Address)
        {
            var existed = await _addressRepository.GetAddressById(Address.Id);

            if(existed == null){
                throw new Exception("");
            }

            existed.Street = Address.Street;
            existed.City = Address.City;
            existed.Country = Address.Country;
            existed.PostalCode = Address.PostalCode;

            await _addressRepository.UpdateAddress(existed);

            return existed;
        }
    }

}