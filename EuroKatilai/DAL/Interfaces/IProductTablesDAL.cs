using DAL.Models;

namespace DAL.Interfaces
{
    public interface IProductTablesDAL
    {
        Task<List<ProductTables>> GetTablesBySeriesId(int id);
    }
}
