using APIREV.DTO;
using APIREV.Models;
using System.Collections.Generic;

namespace APIREV.Services
{
    public interface IEmployeeRepository
    {
        int Create(ForAddingAndRetrivingEmp emp);
        int Delete(int id);
        List<Employee> GetAll();
        Employee GetEmpByID(int id);
        Employee GetEmpByName(string Name);
        int Update(int id, ForAddingAndRetrivingEmp NewEmp);
        EmployeeDataWithDeptNameDTO EmpWithDeptName(int id);
    }
}