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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext companyDbContext;

        public EmployeeRepository(CompanyDbContext companyDbContext):base(companyDbContext)     
        {
            this.companyDbContext = companyDbContext;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return companyDbContext.employees.Where(e=> e.Address == address);
        }
    }
}
