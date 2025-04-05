using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAddress(HttpContext httpContext);
        Task<Address> CreateAddress(AddressDto addressDto, HttpContext httpContext);
        Task<Address> UpdateAddress(Address Address);
        Task<bool> DeleteAddress(int Id);
    }
}