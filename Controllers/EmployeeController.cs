using APIREV.DTO;
using APIREV.Models;
using APIREV.Services;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository EmployeeRepository;

        public EmployeeController(IEmployeeRepository _EmployeeRepository)
        {
            EmployeeRepository = _EmployeeRepository;
        }
        [HttpGet("GetAllEmps")]
        public IActionResult GetAllEmps()
        {
            List<Employee> Emps = EmployeeRepository.GetAll();
            return Ok(Emps);
        }
        [HttpGet("GetEmpByID/{id:int}")]
        public IActionResult GetEmployeeByID(int id)
        {
            Employee E = EmployeeRepository.GetEmpByID(id);
           if (E != null)
                return Ok(E);

            return BadRequest("Id Not Found");
        }
        [HttpPost("CreateNewEmp",Name = "CreateNewEmp")]
        public IActionResult CreateNewEmp(ForAddingAndRetrivingEmp E)
        {
            if (ModelState.IsValid == true)
            {
                EmployeeRepository.Create(E);
                string url = Url.Link("CreateNewEmp", E);
                return Created(url, E);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("EditEmp")]
        public IActionResult EditEmp(int id,ForAddingAndRetrivingEmp NewEmp)
        {
            if (ModelState.IsValid==true)
            {
                EmployeeRepository.Update(id, NewEmp);
                return StatusCode(204, "Data Saved");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteEmp")]
        public IActionResult DeleteEmp(int id)
        {
            try
            {
                EmployeeRepository.Delete(id);
                return Ok("Employee Deleted");
            }
            catch (Exception)
            {

                return BadRequest("ID Not Found");
            }
            

        }
        [HttpGet("ReturnEmpWithDeptName")]
        public IActionResult ReturnEmpWithDeptName(int id)
        {
            if (ModelState.IsValid==true)
            {
                EmployeeDataWithDeptNameDTO Emp =  EmployeeRepository.EmpWithDeptName(id);
                return Ok(Emp);

            }
            return BadRequest(ModelState);
        }
    }

}
