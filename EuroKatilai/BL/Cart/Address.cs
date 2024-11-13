using System.Text.Json;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Profile
{
    public class Address : IAddress
    {
        private readonly IAddressDAL addressDAL;

        public Address(IAddressDAL addressDAL)
        {
            this.addressDAL = addressDAL;
        }

        public async Task<AddressModel> GetAddress(int id) {
            return await addressDAL.Get(id);
        }

        public async Task<AddressModel> GetAddressByUserId(int? userId)
        {
            return await addressDAL.GetAddressByUserId(userId);
        }

        public async Task<int> Save(AddressModel model) {
            if (model.AddressId == null) {
                return await addressDAL.Create(model);
            }
            else {
                await addressDAL.Update(model);
                return model.AddressId ?? 0;
            }
        }
    }
}
