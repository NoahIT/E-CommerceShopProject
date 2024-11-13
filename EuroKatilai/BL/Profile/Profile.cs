using DAL.Interfaces;
using DAL.Models;

namespace BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;
        private readonly IAddressDAL addressDAL;

        public Profile(IProfileDAL profileDAL, IAddressDAL addressDal)
        {
            this.profileDAL = profileDAL;
            this.addressDAL = addressDal;
        }
        public async Task<ProfileModel> Get(int userId)
        {
            return await profileDAL.Get(userId);
        }

        public async Task AddOrUpdate(ProfileModel model,AddressModel addressModel)
        {
            model.AddressModel = addressModel;
            if (model.ProfileId == null)
            {
                var addressId = await addressDAL.Create(addressModel);
                model.AddressId = addressId;
                await profileDAL.Add(model);
            }
            else
            {
                await profileDAL.Update(model);
                await addressDAL.Update(model.AddressModel);
            }
        }
    }
}
