using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using DAL.Models;

namespace EuroKatilai.ViewModels
{
    public class ProfileViewModel : AddressModel
    {
        public int? ProfileId { get; set; }
        public int AddressId { get; set; }
    }
}
