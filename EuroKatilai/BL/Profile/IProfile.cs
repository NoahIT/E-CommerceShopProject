using DAL.Models;

namespace BL.Profile
{
    public interface IProfile
    {
        Task<ProfileModel> Get(int userId);
        Task AddOrUpdate(ProfileModel model, AddressModel addressModel);
    }
}
