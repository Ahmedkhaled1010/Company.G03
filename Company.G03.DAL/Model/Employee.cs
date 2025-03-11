using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Model
{
   public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(50,ErrorMessage ="Max Length is 50 char")]
        [MinLength(5,ErrorMessage ="Min Length is 5 char")]
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
    }
}
