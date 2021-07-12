using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class DepartmentController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get()
        {
            var getdata = db.Departments.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, getdata);
        }

        public HttpResponseMessage Get(int i, int p, string search)
        {
            var count = db.Departments.Count();
            var getdata = db.Departments.ToList().Skip(i * (p - 1)).Take(i);
            if (!String.IsNullOrEmpty(search))
            {
                getdata = db.Departments.Where(x => x.DepartmentName.Contains(search)).ToList().Skip(i * (p - 1)).Take(i);
                count = db.Departments.Where(x => x.DepartmentName.Contains(search)).Count();
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { getdata, count });

        }
        public string Post([FromBody] Department dep)
        {
            try
            {
                Departments dept = new Departments();

                dept.DepartmentName = dep.DepartmentName;

                db.Departments.Add(dept);
                db.SaveChanges();

                return "Department Added Successfully";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string Put([FromBody] Department dep)
        {
            try
            {
                var dept = db.Departments.Where(x => x.DeparmentID == dep.DeparmentID).FirstOrDefault();

                dept.DepartmentName = dep.DepartmentName;

                db.SaveChanges();

                return "Department Updated Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }

        public string Delete(long id)
        {
            try
            {
                var dept = db.Departments.Where(x => x.DeparmentID == id).FirstOrDefault();

                db.Departments.Remove(dept);

                db.SaveChanges();

                return "Department Deleted Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }

    }
}
