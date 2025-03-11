﻿using Company.G03.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
       IQueryable<Employee> GetEmployeesByAddress(string address);
    }
}
