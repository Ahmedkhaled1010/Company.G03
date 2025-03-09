using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Model
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage =" Name Is Required")]

        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
