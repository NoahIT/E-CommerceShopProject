
using DAL;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Cart
{
    public class AddressDAL : IAddressDAL
    {
        private readonly IDbHelper dbHelper;

        public AddressDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> Create(AddressModel model) 
        {
            string sql = @"insert into `Address` (UserId, Country, Region, City, ZipCode, Street, `Status`, House, Appartment, RecieverName, RecieverSurname, Phone, Email, Created, Modified, `Code`, PVMCode, CompanyName, Delivery)
                    values (@UserId, @Country, @Region, @City, @ZipCode, @Street, @Status, @House, @Appartment, @RecieverName, @RecieverSurname, @Phone, @Email, @Created, @Modified, @Code, @PVMCode, @CompanyName, @Delivery);
                    SELECT LAST_INSERT_ID();";

            return await dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<AddressModel> Get(int addressid)
        {
            string sql = @"select AddressId, UserId, Region, City, ZipCode, Street, `Status`, House,
                            Appartment, RecieverName,RecieverSurname, Phone, Email, Created,Country,`Code`,PVMCode,CompanyName,Delivery, Modified 
                            from `Address`
                            where AddressId = @addressid";
            var addresses = await dbHelper.QueryAsync<AddressModel>(sql, new { addressid = addressid });
            return addresses.FirstOrDefault() ?? new AddressModel();
        }

        public async Task Update(AddressModel sessionData)
        {
            string sql = @"update `Address`
                    set 
                        Region = @Region,
                        City = @City,
                        ZipCode = @ZipCode,
                        Street = @Street,
                        `Status` = @Status,
                        House = @House,
                        Appartment = @Appartment,
                        RecieverName = @RecieverName,
                        RecieverSurname = @RecieverSurname,
                        Phone = @Phone,
                        Email = @Email,
                        Modified = @Modified,
                        Country = @Country,
                        `Code` = @Code,
                        `PVMCode` = @PVMCode,
                        `CompanyName` = @CompanyName,
                         Delivery = @Delivery

                    where AddressId = @AddressId";

            await dbHelper.ExecuteAsync(sql, sessionData);
        }

        public async Task Delete(int addressid)
        {
            string sql = @"delete from `Address` where AddressId = @addressid";

            await dbHelper.ExecuteAsync(sql, new { addressid = addressid });
        }

        public async Task<AddressModel> GetAddressByUserId(int? userId)
        {
            string sql = @"select AddressId, UserId, Region, City, ZipCode, Street, `Status`, House,
                            Appartment, RecieverName, RecieverSurname, Phone, Email, Created,Country,`Code`,PVMCode,CompanyName,Delivery, Modified 
                            from `Address`
                            where UserId = @userId";

            var addresses = await dbHelper.QueryAsync<AddressModel>(sql, new { userId = userId });
            return addresses.FirstOrDefault() ?? new AddressModel();
        }
    }
}

