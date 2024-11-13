using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace BL.Adds
{
    public class Reklama : IReklama
    {
        private readonly IAddsDAL addsDAL;

        public Reklama(IAddsDAL addsDal)
        {
            addsDAL = addsDal;
        }


        public async Task<List<string>> GetAllPathsToPhotos()
        {
            var adds = await addsDAL.GetAllAdds();

            return adds.Select(x => x.Photo).ToList();
        }
    }
}
