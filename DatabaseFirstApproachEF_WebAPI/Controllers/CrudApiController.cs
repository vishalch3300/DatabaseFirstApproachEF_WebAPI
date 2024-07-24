using DatabaseFirstApproachEF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseFirstApproachEF_WebAPI.Controllers
{
    public class CrudApiController : ApiController
    {
        web_api_crud_dbEntities db = new web_api_crud_dbEntities();

        //READ-------------------------------------------------------------------------------------
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            List<Employee> list = db.Employees.ToList();
            return Ok(list);
        }

        //CREATE-----------------------------------------------------------------------------------
        [HttpPost]
        public IHttpActionResult EmpInsert(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
            return Ok();
        }

        //READ by Id -- Details + Update + Delete-------------------------------------------------------------
        [HttpGet]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var emp = db.Employees.Where(model => model.id == id).FirstOrDefault();
            return Ok(emp);
        }

        //Update-----------------------------------------------------------------------------------
        [HttpPut]
        public IHttpActionResult EmpUpdate(Employee e)
        {
            //Logic 1
            /*
            var emp = db.Employees.Where(model => model.id == e.id).FirstOrDefault();
            if (emp != null)
            {
                emp.id = e.id;
                emp.name = e.name;
                emp.gender = e.gender;
                emp.age = e.age;
                emp.designation = e.designation;
                emp.salary = e.salary;

                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            */

            //Logic 2
            db.Entry(e).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok();
        }

        //Delete-----------------------------------------------------------------------------------
        [HttpDelete]
        public IHttpActionResult EmpDelete(int id)
        {
            var emp = db.Employees.Where(model => model.id == id).FirstOrDefault();

            db.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
