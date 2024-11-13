using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set;}

        [Required(ErrorMessage = "Slaptažodis privalo būti įvestas")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Jūsų slaptažodis yra silpnas. Jis turi būti mažiausiai 8 simbolių ilgio ir turėti bent vieną mažąją raidę, vieną didžiąją raidę ir vieną skaitmenį.")]
        public string NewPassword { get; set;}
        [Required]
        public string NewPassword2 { get; set;}
    }
}
