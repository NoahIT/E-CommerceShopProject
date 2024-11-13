using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBrandDAL
    {
        Task<List<BrandModel>> GetBrandsByClass(string _class);
        //Task<BrandModel> GetBrandById(string _id);
    }
}
