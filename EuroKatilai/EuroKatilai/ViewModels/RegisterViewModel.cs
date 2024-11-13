using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EuroKatilai.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El.paštas privalo būti įvestas")]
        [EmailAddress(ErrorMessage = "Netinkamas El.pašto formatas")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Slaptažodis privalo būti įvestas")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Jūsų slaptažodis yra silpnas. Jis turi būti mažiausiai 8 simbolių ilgio ir turėti bent vieną mažąją raidę, vieną didžiąją raidę ir vieną skaitmenį.")]
        public string? Password { get; set; }

        public string? Country { get; set; }

        public string? Region { get; set; }

        public string? City { get; set; }

        public string? ZipCode { get; set; }

        public string? Street { get; set; }

        public string? House { get; set; }

        public string? Appartment { get; set; }

        public string? Code { get; set; }
        public string? PVMCode { get; set; }
        public string? CompanyName { get; set; }

        public string? RecieverName { get; set; }
        public string? RecieverSurname { get; set; }

        public string? Phone { get; set; }

        public string? Delivery { get; set; } = "";

        public int Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}
