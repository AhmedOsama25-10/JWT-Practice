using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIREV.DTO
{
    public class DepartmentDataWithEmplyeeListDTO
    {
        public string Name { get; set; }
        public string MangerName { get; set; }
        public List<string> EmployeeNames { get; set; } = new();
    }
}
