using DAL.Interfaces;
using DAL.Models;
using Newtonsoft.Json;

namespace DAL.Product
{
    public class ProductTablesDAL : IProductTablesDAL
    {
        private readonly IDbHelper dbHelper;

        public ProductTablesDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }


        public async Task<List<ProductTables>> GetTablesBySeriesId(int id)
        {
            string sql = "select * from ProductTables where SeriesId = @id";

            var tables = await dbHelper.QueryAsync<ProductTables>(sql, new { id });

            var x = tables.ToList();

            x.ForEach(x =>
            {
                x.ModelsIntList = JsonConvert.DeserializeObject<List<int>>(x.ModelsId) ?? new List<int>();
            });

            return x ?? new List<ProductTables>();
        }
    }
}
