using DAL.Models;

namespace BL.Brand
{
    public interface IBrand
    {
        Task<List<BrandModel>> GetBrandsByClass(string _class);
    }
}
