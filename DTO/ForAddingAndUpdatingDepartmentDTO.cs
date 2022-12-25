using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIREV.DTO
{
    public class ForAddingAndUpdatingDepartmentDTO
    {
        [Required]
        public string Name { get; set; }
        public string MangerName { get; set; }
    }
}
