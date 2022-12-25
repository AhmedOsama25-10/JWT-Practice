using APIREV.DTO;
using APIREV.Models;
using System.Collections.Generic;

namespace APIREV.Services
{
    public interface IDepartmentRepository
    {
        int Create(ForAddingAndUpdatingDepartmentDTO NewDept);
        int Delete(int id);
        DepartmentDataWithEmplyeeListDTO DeptWitEmpList(int id);
        List<Department> GetAll();
        Department GetDeptById(int id);
        Department GetDeptByName(string Name);
        int Update(int id, ForAddingAndUpdatingDepartmentDTO EditedDept);
    }
}