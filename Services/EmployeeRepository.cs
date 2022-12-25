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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext Context;

        public EmployeeRepository(AppDbContext _context)
        {
            Context = _context;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = Context.Employees.ToList();
            return employees;

        }
        public Employee GetEmpByID(int id)
        {
            Employee emp = Context.Employees.FirstOrDefault(e => e.ID == id);
            return emp;
        }
        public Employee GetEmpByName(string Name)
        {
            Employee emp = Context.Employees.FirstOrDefault(e => e.Name == Name);
            return emp;
        }
        public int Create(ForAddingAndRetrivingEmp emp)
        {
            Employee E = new Employee()
            {
                Address = emp.Address,
                Name = emp.Name,
                DeptID = emp.DeptID,
                Phone = emp.Phone,
                Salary = emp.Salary,
              
            };
            Context.Employees.Add(E);
            int raw = Context.SaveChanges();
            return raw;
        }
        public int Update(int id, ForAddingAndRetrivingEmp NewEmp)
        {
            int raw = 0;
            Employee OldEmp = Context.Employees.FirstOrDefault(e => e.ID == id);
            if (OldEmp != null)
            {
                OldEmp.Name = NewEmp.Name;
                OldEmp.Phone = NewEmp.Phone;
                OldEmp.Salary = NewEmp.Salary;
                OldEmp.DeptID = NewEmp.DeptID;
                OldEmp.Address = NewEmp.Address;
                raw = Context.SaveChanges();

            }
            return raw;
        }
        public int Delete(int id)
        {
            try
            {
                Employee E = Context.Employees.FirstOrDefault(e => e.ID == id);
                Context.Employees.Remove(E);
                int raw = Context.SaveChanges();
                return raw;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public EmployeeDataWithDeptNameDTO EmpWithDeptName(int id)
        {
            Employee Emp = Context.Employees.Include(E => E.Department).FirstOrDefault(E => E.ID == id);
            if (Emp!=null)
            {
                EmployeeDataWithDeptNameDTO EmpDept = new EmployeeDataWithDeptNameDTO();
                EmpDept.Address = Emp.Address;
                EmpDept.DeptID = Emp.DeptID;
                EmpDept.Name = Emp.Name;
                EmpDept.Phone = Emp.Phone;
                EmpDept.Salary = Emp.Salary;
                EmpDept.DeptName = Emp.Department.Name;
                return EmpDept;
            }
            return new EmployeeDataWithDeptNameDTO();
            
        }
    }
}
