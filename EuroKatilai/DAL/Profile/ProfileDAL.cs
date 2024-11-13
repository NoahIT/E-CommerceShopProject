using DAL.Interfaces;
using DAL.Models;

namespace DAL.Profile
{
    public class ProfileDAL : IProfileDAL
    {
        private readonly IDbHelper dbHelper;

        public ProfileDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }


        public async Task<int> Add(ProfileModel profile)
        {
            string sql = @"insert into Profile(UserId, FirstName, LastName,AddressId)
                    values(@UserId, @FirstName, @LastName,@AddressId);
                    SELECT LAST_INSERT_ID();";
            var result = await dbHelper.QueryAsync<int>(sql, profile);
            return result.First();
        }

        public async Task<ProfileModel> Get(int userId)
        {
            var result = await dbHelper.QueryAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, FirstName, LastName, AddressId
                        from Profile
                        where UserId = @id", new { id = userId });
            return  result.FirstOrDefault() ?? new ProfileModel();
        }

        public async Task Update(ProfileModel profile)
        {
            string sql = @"Update Profile
                    set FirstName = @FirstName,
                        LastName = @LastName,
                        AddressId = @AddressId
                    where ProfileId = @ProfileId";
            var result = await dbHelper.QueryAsync<int>(sql, profile);
        }

        public async Task<ProfileModel> GetByUserId(int userId)
        {
            return await dbHelper.QueryScalarAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, FirstName, LastName, AddressId 
                        from Profile
                        where UserId = @id", new { id = userId });
        }
    }
}
