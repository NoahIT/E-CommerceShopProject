using DAL.Interfaces;
using DAL.Models;

namespace DAL.Product
{
    public class BrandDAL : IBrandDAL
    {
        private readonly IDbHelper dbHelper;

        public BrandDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<List<BrandModel>> GetBrandsByClass(string _class)
        {
            string sql = @"select * from Brand where Class = @klase";

            var x = await dbHelper.QueryAsync<BrandModel>(sql, new { klase = _class });

            return x.ToList() ?? new List<BrandModel>();
        }
    }
}
