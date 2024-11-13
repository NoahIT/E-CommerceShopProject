using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserPasswordSalt
    {
        public string Password { get; set; } 
        public string Salt { get; set; }
    }
}
