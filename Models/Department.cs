using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIREV.Models
{
    public class Department
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string MangerName { get; set; }
        public List<Employee>  Employees { get; set; }
    }
}