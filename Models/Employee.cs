using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIREV.Models
{
    public class Employee
    {
        
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public long Phone { get; set; }
        public double  Salary { get; set; }
        public string Address { get; set; }
        [ForeignKey("Department")]
        public int DeptID { get; set; }
        public Department Department { get; set; }
    }
}
