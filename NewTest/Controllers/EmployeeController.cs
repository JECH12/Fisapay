using Domain;
using NewTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewTest.Controllers
{
    public class EmployeeController : ApiController
    {
        Business.Employee admin = new Business.Employee();

        [Route("api/Employees")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = admin.GetEmployees();

            return employees;
        }

        [Route("api/Employees/{searchTerm}")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployees(string searchTerm)
        {
            var employees = admin.GetEmployees(searchTerm);

            return employees;
        }

        [Route("api/AddEmployee")]
        [HttpGet]
        public EditEmployeeModel AddEmployee()
        {
            var model = new EditEmployeeModel();

            return model;
        }

        [Route("api/AddEmployee")]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            admin.AddEmployee(employee);

            return StatusCode(HttpStatusCode.OK);
        }

        [Route("api/DeleteEmployee/{employeeId}")]
        [HttpPost]
        public void DeleteCourse(Guid employeeId)
        {
            admin.DeleteEmployee(employeeId);
        }

        [Route("api/EditEmployee/{employeeId}")]
        [HttpGet]
        public EditEmployeeModel EditCourse(Guid employeeId)
        {
            var model = new EditEmployeeModel();
            model.Employee = admin.GetEmployee(employeeId);
            return model;
        }

        [Route("api/EditEmployee")]
        [HttpPost]
        public IHttpActionResult EditEmployee(Employee employee)
        {
            admin.EditEmployee(employee);
            return StatusCode(HttpStatusCode.OK);
        }

        [Route("api/VerifyEmployee/{employeeDNI}")]
        [HttpGet]
        public bool VerifyEmployee(string employeeDNI)
        {
            var result = admin.VerifyEmployee(employeeDNI);
            if (result)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
