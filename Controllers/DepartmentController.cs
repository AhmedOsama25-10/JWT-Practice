using APIREV.DTO;
using APIREV.Models;
using APIREV.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIREV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository DepartmentRepository;

        public DepartmentController(IDepartmentRepository _DepartmentRepository )
        {
            DepartmentRepository = _DepartmentRepository;
        }
        [HttpGet("GetAllDept")]
        public IActionResult GetAllDept()
        {
           List<Department> Depts =  DepartmentRepository.GetAll();

            return Ok(Depts);
        }
      
        [HttpGet("GetDeptWithId/{id:int}")]
        public IActionResult GetDeptWithId(int id)
        {

           Department Dept =  DepartmentRepository.GetDeptById(id);
            if (Dept!= null)
                return Ok(Dept);
            return BadRequest("Id Not Found");
            
        }
        [HttpGet("GetDeptWithName/{Name:alpha}")]

        public IActionResult GetDeptWithName(string Name)
        {
            Department Dept = DepartmentRepository.GetDeptByName(Name);
            if (Dept !=null)
                return Ok(Dept);
            
            return BadRequest("Name Not Found");
        }
        [HttpPost("CreateNewDept")]
        public IActionResult CreateNewDept(ForAddingAndUpdatingDepartmentDTO Dep)
        {
            if (ModelState.IsValid==true)
            {
                DepartmentRepository.Create(Dep);
                return Ok(Dep);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("EditDept/{id:int}")]
        public IActionResult EditDept(int id , ForAddingAndUpdatingDepartmentDTO Dep)
        {
            if (ModelState.IsValid == true)
            {
                DepartmentRepository.Update(id, Dep);
                return Ok("Updated");

            }
            return BadRequest("Id Not Found");
        }
        [HttpDelete("DeleteDep/{id:int}")]
        public IActionResult DeleteDep(int id)
        {
            try
            {
                DepartmentRepository.Delete(id);
                return Ok("Deketed");

            }
            catch (Exception)
            {

                return BadRequest("Id Not found");
            }
            
            
        }
        [HttpGet("GetDeptsWithEmpNames/{id:int}")]
        public IActionResult GetDeptsWithEmpNames(int id)
        {
         DepartmentDataWithEmplyeeListDTO DeptWithEmps =    DepartmentRepository.DeptWitEmpList(id);
            if (DeptWithEmps !=null)
                return Ok(DeptWithEmps);
            return BadRequest("ID Not Found");
        }
    }
}
