using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IAddressRepository{
        Task<IEnumerable<Address>> GetAllAddress();
        Task<Address> GetAddressById(int Id);
        Task<IEnumerable<Address>> GetAddressByUserId(int Id);
        Task<Address> CreateAddress(Address Address);
        Task<Address> UpdateAddress(Address Address);
        Task<bool> DeleteAddress(Address address);

    }

}