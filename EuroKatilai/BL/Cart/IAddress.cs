
using DAL.Models;

namespace BL.Profile {
    public interface IAddress {
        public Task<int> Save(AddressModel model);
        public Task<AddressModel> GetAddress(int id);
        public Task<AddressModel> GetAddressByUserId(int? userId);
    }
}