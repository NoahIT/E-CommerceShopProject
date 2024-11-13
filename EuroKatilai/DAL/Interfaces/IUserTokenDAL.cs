using System;

namespace DAL.Interfaces
{
    public interface IUserTokenDAL
    {
        Task<Guid> Create(int userId);

        Task<int?> Get(Guid tokenid);
        Task DeleteToken(Guid tokenid);
    }
}

