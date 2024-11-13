using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Adds
{
    public class AddsDAL : IAddsDAL
    {
        private readonly IDbHelper dbHelper;

        public AddsDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<List<Models.Adds>> GetAllAdds()
        {
            string sql = @"select * from Reklama";

            return (await dbHelper.QueryAsync<Models.Adds>(sql,new {})).ToList();
        }
    }
}
