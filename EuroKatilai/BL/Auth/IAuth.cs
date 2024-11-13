using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel model);
        Task<int> Authenticate(string email, string password, bool rememberme);
        Task ValidateEmail(string email);
        Task<int> Register(UserModel user);
    }
}
