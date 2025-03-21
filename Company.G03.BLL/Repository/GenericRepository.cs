using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _companyDbContext;
        public GenericRepository(CompanyDbContext companyDbContext)
        {
            _companyDbContext = companyDbContext;
        }
        {
        }

        {
            _companyDbContext.Remove(item);
            return _companyDbContext.SaveChanges();
        }

        {
        }

        {
            if (typeof(T) ==typeof(Employee))
            {

            }
        }

        {
            _companyDbContext.Update(item);
        }
    }
}
