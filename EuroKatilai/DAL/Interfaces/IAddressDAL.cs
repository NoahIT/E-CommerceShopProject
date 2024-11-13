using DAL.Models;

namespace DAL.Interfaces
{
	public interface IAddressDAL
	{
        public Task<int> Create(AddressModel model);
        public Task<AddressModel> Get(int addressid);
        public Task Update(AddressModel sessionData);
        public Task Delete(int addressid);
        Task<AddressModel> GetAddressByUserId(int? userId);

    }
}