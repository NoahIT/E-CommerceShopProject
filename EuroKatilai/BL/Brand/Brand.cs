using DAL.Interfaces;
using DAL.Models;

namespace BL.Brand
{
    public class Brand : IBrand
    {
        private readonly IBrandDAL brandDal;
        public Brand(IBrandDAL brandDal)
        {
            this.brandDal = brandDal;
        }


        public async Task<List<BrandModel>> GetBrandsByClass(string _class)
        {
            return await brandDal.GetBrandsByClass(_class);
        }
    }
}
