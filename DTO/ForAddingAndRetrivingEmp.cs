using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIREV.DTO
{
    public class ForAddingAndRetrivingEmp
    {
        [Required]
        public string Name { get; set; }
        public long Phone { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }
        public int DeptID { get; set; }
    }
}
