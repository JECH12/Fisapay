using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Employee
    {
        public IEnumerable<Domain.Employee> GetEmployees()
        {
            var db = new DatabaseContext();

            var employees = db.Employees.ToList().OrderBy(x => x.Name);

            return employees;
        }

        public IEnumerable<Domain.Employee> GetEmployees(string searchTerm)
        {
            var db = new DatabaseContext();

            var employees = (from e in db.Employees
                             where (searchTerm == null || e.Name.Contains(searchTerm) || e.DNI.Contains(searchTerm))
                             select e).ToList();

            return employees;
        }

        public void AddEmployee(Domain.Employee employee)
        {

            var db = new DatabaseContext();

            employee.Id = Guid.NewGuid();

            db.Employees.Add(employee);

            db.SaveChanges();
        }

        public void EditEmployee(Domain.Employee employee)
        {
            var db = new DatabaseContext();

            var oldEmployee = db.Employees.FirstOrDefault(x => x.Id == employee.Id);

            oldEmployee.Id            = employee.Id;
            oldEmployee.Name          = employee.Name;
            oldEmployee.Sex           = employee.Sex;
            oldEmployee.Birthday      = employee.Birthday;
            oldEmployee.Salary        = employee.Salary;
            oldEmployee.Covid_Vaccine = employee.Covid_Vaccine;

            db.SaveChanges();
        }

        public void DeleteEmployee(Guid employeeId)
        {
            var db = new DatabaseContext();
            var employeeToDelete = db.Employees.FirstOrDefault(x => x.Id == employeeId);
            db.Employees.Remove(employeeToDelete);

            db.SaveChanges();
        }


        public Domain.Employee GetEmployee(Guid employeeId)
        {
            var db = new DatabaseContext();
            var course = db.Employees.FirstOrDefault(x => x.Id == employeeId);

            return course;
        }

        public bool VerifyEmployee(string employeeDNI)
        {
            var db = new DatabaseContext();

            var employee = db.Employees.FirstOrDefault(x => x.DNI == employeeDNI);

            if (employee != null)
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
