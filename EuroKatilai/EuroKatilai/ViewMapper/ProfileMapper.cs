using DAL.Models;
using EuroKatilai.ViewModels;

namespace EuroKatilai.ViewMapper
{
    public class ProfileMapper
    {
        public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        {
            return new ProfileModel()
            {
                ProfileId = model.ProfileId,
                AddressId = model.AddressId
            };
        }
        public static AddressModel MapProfileViewModelToAddressModel(ProfileViewModel model)
        {
            return new AddressModel()
            {
                AddressId = model.AddressId,
                Appartment = model.Appartment,
                City = model.City,
                Country = model.Country,
                Code = model.Code,
                PVMCode = model.PVMCode,
                Email = model.Email,
                CompanyName = model.CompanyName,
                Delivery = model.Delivery,
                Modified = DateTime.UtcNow,
                ZipCode = model.ZipCode,
                House = model.House,
                RecieverSurname = model.RecieverSurname,
                RecieverName = model.RecieverName,
                Phone = model.Phone,
                Status = model.Status,
                Street = model.Street,
                Region = model.Region
            };
        }

        public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        {
            return new ProfileViewModel()
            {
                ProfileId = model.ProfileId,
                AddressId = model.AddressId ?? 0,

                Appartment = model.AddressModel.Appartment,
                City = model.AddressModel.City,
                Country = model.AddressModel.Country,
                Code = model.AddressModel.Code,
                PVMCode = model.AddressModel.PVMCode,
                Email = model.AddressModel.Email,
                CompanyName = model.AddressModel.CompanyName,
                Delivery = model.AddressModel.Delivery,
                Modified = DateTime.UtcNow,
                ZipCode = model.AddressModel.ZipCode,
                House = model.AddressModel.House,
                RecieverSurname = model.AddressModel.RecieverSurname,
                RecieverName = model.AddressModel.RecieverName,
                Phone = model.AddressModel.Phone,
                Status = model.AddressModel.Status,
                Street = model.AddressModel.Street,
                Region = model.AddressModel.Region
            };
        }

        public static AddressModel MapAddressViewModelToAddressModel(AddressViewModel model)
        {
            return new AddressModel()
            {
                AddressId = model.AddressId,
                Region = model.Region,
                City = model.City,
                ZipCode = model.ZipCode,
                Street = model.Street,
                House = model.House,
                Appartment = model.Appartment,
                RecieverName = model.RecieverName,
                RecieverSurname = model.RecieverSurname,
                Phone = model.Phone,
                Email = model.Email,
                Country = model.Country,
                PVMCode = model.PVMCode,
                Code = model.Code,
                CompanyName = model.CompanyName,
                Delivery = model.Delivery,
                Status = (int)model.Status
            };
        }

        public static AddressViewModel MapAddressModelToAddressViewModel(AddressModel model)
        {
            return new AddressViewModel()
            {
                AddressId = model.AddressId,
                Region = model.Region,
                City = model.City,
                ZipCode = model.ZipCode,
                Street = model.Street,
                House = model.House,
                Appartment = model.Appartment,
                RecieverName = model.RecieverName,
                RecieverSurname = model.RecieverSurname,
                Phone = model.Phone,
                Email = model.Email,
                Country = model.Country,
                PVMCode = model.PVMCode,
                Code = model.Code,
                CompanyName = model.CompanyName,
                Delivery = model.Delivery,
                Status = (AddressViewModel.AddressStatusEnum)model.Status,
            };
        }
    }
}
