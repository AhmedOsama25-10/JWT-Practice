using APIREV.ApplicationDbContext;
using APIREV.DTO;
using APIREV.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIREV.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext Context;

        public DepartmentRepository(AppDbContext _Context)
        {
            Context = _Context;
        }
        public List<Department> GetAll()
        {
            return Context.Departments.ToList();
        }
        public Department GetDeptById(int id)
        {
            Department dept = Context.Departments.FirstOrDefault(D => D.ID == id);
            return dept;

        }
        public Department GetDeptByName(string Name)
        {
            Department dept = Context.Departments.FirstOrDefault(D => D.Name == Name);
            return dept;

        }
        public int Create(ForAddingAndUpdatingDepartmentDTO NewDept)
        {
            Department Dept = new Department()
            {
                Name = NewDept.Name,
                MangerName = NewDept.MangerName
            };

            Context.Departments.Add(Dept);
            int raw = Context.SaveChanges();
            return raw;
        }
        public int Update(int id, ForAddingAndUpdatingDepartmentDTO EditedDept)
        {
            Department dept = Context.Departments.FirstOrDefault(D => D.ID == id);
            dept.Name = EditedDept.Name;
            dept.MangerName = EditedDept.MangerName;
            int raw = Context.SaveChanges();
            return raw;
        }
        public int Delete(int id)
        {
            try
            {
                Department dept = Context.Departments.FirstOrDefault(D => D.ID == id);
                Context.Departments.Remove(dept);
                int raw = Context.SaveChanges();
                return raw;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public DepartmentDataWithEmplyeeListDTO DeptWitEmpList(int id)
        {
            Department dept = Context.Departments.Include(D => D.Employees).FirstOrDefault(D => D.ID == id);
            DepartmentDataWithEmplyeeListDTO DeptWithEmps = new DepartmentDataWithEmplyeeListDTO();
            DeptWithEmps.Name = dept.Name;
            DeptWithEmps.MangerName = dept.MangerName;
            foreach (var item in dept.Employees)
            {
                DeptWithEmps.EmployeeNames.Add(item.Name);
            }
            return DeptWithEmps;

        }

    }
}
