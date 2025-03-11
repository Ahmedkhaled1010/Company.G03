using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;

namespace Company.G03.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _companyDbContext;
        public GenericRepository(CompanyDbContext companyDbContext)
        {
            _companyDbContext = companyDbContext;
        }
        public int Add(T item)
        {
            _companyDbContext.Add(item);
            return _companyDbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _companyDbContext.Remove(item);
            return _companyDbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _companyDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _companyDbContext.Set<T>().ToList();
        }

        public int Update(T item)
        {
            _companyDbContext.Update(item);
            return _companyDbContext.SaveChanges();
        }
    }
}
