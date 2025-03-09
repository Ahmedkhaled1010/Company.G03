using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repository;
using Company.G03.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanyDbContext>(option =>
            {
                option.UseSqlServer("Server = DESKTOP-VSQSLHO\\MSSQLSERVER2 ; Database = CompanyMVC; Trusted_Connection = True; TrustServerCertificate = True");

            });
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();

            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
