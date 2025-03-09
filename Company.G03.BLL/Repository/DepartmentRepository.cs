using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _dbContext;
        public DepartmentRepository(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Department department)
        {
            _dbContext.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Remove(department);
            return _dbContext.SaveChanges();
        }

        public Department GetById(int id)
        {
            //var dept= _dbContext.departments.Local.Where(d=>d.Id == id).FirstOrDefault();
            // if(dept is null)
            // {
            //     dept= _dbContext.departments.Where(d => d.Id == id).FirstOrDefault();
            // }
            // return dept;
            return _dbContext.departments.Find(id);

        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.departments.ToList();
        }

        public int Update(Department department)
        {
            _dbContext.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
