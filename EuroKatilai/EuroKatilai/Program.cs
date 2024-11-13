using BL.Adds;
using BL.Auth;
using BL.Brand;
using BL.Cart;
using BL.Email;
using BL.General;
using BL.Products;
using BL.Profile;
using BL.Series;
using DAL;
using DAL.Adds;
using DAL.AdminPanel;
using DAL.Auth;
using DAL.Cart;
using DAL.Email;
using DAL.Interfaces;
using DAL.Product;
using DAL.Profile;
using EuroKatilai.Areas.Admin.BL;
using EuroKatilai.Service;
using Org.BouncyCastle.Utilities.Collections;

namespace EuroKatilai
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IAuth, Auth>();
            builder.Services.AddScoped<IEncrypt, Encrypt>();
            builder.Services.AddScoped<IDbSession, DbSession>();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IWebCookie, WebCookie>();
            builder.Services.AddScoped<IProfile, Profile>();
            builder.Services.AddScoped<ISeries, Series>();
            builder.Services.AddScoped<IBrand, Brand>();
            builder.Services.AddScoped<IProducts, Products>();
            builder.Services.AddScoped<ICart, Cart>();
            builder.Services.AddScoped<IAddress, Address>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IOrder, Order>();
            builder.Services.AddScoped<IWebPhoto, WebPhoto>();
            builder.Services.AddScoped<IWebFile, WebFile>();
            builder.Services.AddScoped<IReklama, Reklama>();



            builder.Services.AddScoped<IAuthDAL, AuthDAL>();
            builder.Services.AddScoped<IDbSessionDAL, DbSessionDAL>();
            builder.Services.AddScoped<IUserTokenDAL, UserTokenDAL>();
            builder.Services.AddScoped<IProfileDAL, ProfileDAL>();
            builder.Services.AddScoped<ISeriesDAL, SeriesDAL>();
            builder.Services.AddScoped<IBrandDAL, BrandDAL>();
            builder.Services.AddScoped<IProductDAL, ProductDAL>();
            builder.Services.AddScoped<IProductTablesDAL, ProductTablesDAL>();
            builder.Services.AddScoped<ICartDAL, CartDAL>();
            builder.Services.AddScoped<IAddressDAL, AddressDAL>();
            builder.Services.AddScoped<IOrderDAL, OrderDAL>();
            builder.Services.AddScoped<IProductSearchDAL, ProductSearchDAL>();
            builder.Services.AddScoped<IAdminPanelDAL, AdminPanelDAL>();
            builder.Services.AddScoped<IAddsDAL, AddsDAL>();
            builder.Services.AddScoped<IDbHelper, DbHelper>();
            builder.Services.AddScoped<IEmailHostinger, EmailHostinger>();


            builder.Services.AddMvc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error404");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "area",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            DbHelper.ConnString = app.Configuration["ConnectionStrings:Default"] ?? "";

            app.Run();
            
        }
    }
}