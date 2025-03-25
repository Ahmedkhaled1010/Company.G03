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
        public async Task AddAsync(T item)
        {
           await _companyDbContext.AddAsync(item);
     
        }

        public void Delete(T item)
        {
            _companyDbContext.Remove(item);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await  _companyDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) ==typeof(Employee))
            {
                return  (IEnumerable<T>) await _companyDbContext.employees.Include(e=>e.Department).ToListAsync();

            }
            return await _companyDbContext.Set<T>().ToListAsync();
        }

        public void Update(T item)
        {
            _companyDbContext.Update(item);
          
        }
    }
}
