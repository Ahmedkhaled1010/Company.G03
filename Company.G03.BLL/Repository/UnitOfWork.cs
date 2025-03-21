using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;

namespace Company.G03.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly CompanyDbContext dbContext;

        public IDepartmentRepository DepartmentRepository { get ; set; }
        public IEmployeeRepository EmployeeRepository { get ; set; }
        public UnitOfWork(CompanyDbContext dbContext) 
        {
            EmployeeRepository=new EmployeeRepository(dbContext);
            DepartmentRepository=new DepartmentRepository(dbContext);
            this.dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
          return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose( );
        }
    }
}
