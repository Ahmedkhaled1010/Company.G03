using Company.G03.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Data.Contexts
{
    public class CompanyDbContext: DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options) 
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}
      public  DbSet<Department> departments {  get; set; }
    }
}
