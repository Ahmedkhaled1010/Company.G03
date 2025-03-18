using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Model
{
   public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(22,35,ErrorMessage ="Age Must be In Range From 22 To 35")]
        public int Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        [ForeignKey("Department")]
        public int?  DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department? Department { get; set; }
    }
}
