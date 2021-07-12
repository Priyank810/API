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
    public class EmployeeController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get(string em)
        {
            var result = db.Employees.Where(x => x.MailID == em).FirstOrDefault();
            if(result!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Email Already Exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        public HttpResponseMessage Get(int id)
        {

            var i = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            
            Employee em = new Employee();
            
            em.EmployeeID = i.EmployeeID;
            em.EmployeeName = i.EmployeeName;
            em.Department = i.Department;
            em.MailID = i.MailID;
            em.DOJ = i.DOJ;
            em.gender = i.gender;
            em.primaryaddress = i.primaryaddress;
            em.secondaryaddress = i.secondaryaddress;
            em.imgFile = i.imgFile;

            var gethobby = db.Hobby.Where(x => x.userid == i.EmployeeID).ToList();
            if (gethobby.Count != 0)
            {
                List<string> temp = new List<string>();
                foreach (var j in gethobby)
                {
                    temp.Add(j.hobby1);
                }
                em.hobby = temp;
            }
            else
            {
                em.hobby = null;
            }

            var getexperience = db.UserExperience.Where(x => x.employeeid == i.EmployeeID).ToList();
            if (getexperience.Count != 0)
            {
                List<exp> ex = new List<exp>();
                foreach (var k in getexperience)
                {
                    exp t = new exp();
                    t.technology = k.technology;
                    t.startdate = (DateTime)k.startdate;
                    t.enddate = (DateTime)k.enddate;
                    ex.Add(t);
                }
                em.experience = ex;
            }
            else
            {
                em.experience = null;
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, em);
        }

        public HttpResponseMessage Get(int items, int page, string search, string order)
        {

            var count = db.Employees.Count();
            var getdata = db.Employees.ToList(); 


            if (!String.IsNullOrEmpty(search))
            {
                getdata = db.Employees.Where(x => x.EmployeeName.Contains(search)).ToList(); 
                count = db.Employees.Where(x => x.EmployeeName.Contains(search)).Count();
            }

            List<Employee> employeelist = new List<Employee>();

            foreach(var i in getdata)
            {
                Employee em = new Employee();
                em.EmployeeID = i.EmployeeID;
                em.EmployeeName = i.EmployeeName;
                em.Department = i.Department;
                em.MailID = i.MailID;
                em.DOJ = i.DOJ;
                em.gender = i.gender;
                em.primaryaddress = i.primaryaddress;
                em.secondaryaddress = i.secondaryaddress;
                em.imgFile = i.imgFile;

                var gethobby = db.Hobby.Where(x => x.userid == i.EmployeeID).ToList();
                if(gethobby.Count != 0)
                {
                    List<string> temp = new List<string>();
                    foreach (var j in gethobby)
                    {
                        temp.Add(j.hobby1);
                    }
                    em.hobby = temp;
                }
                else
                {
                    em.hobby = null;
                }

                var getexperience = db.UserExperience.Where(x => x.employeeid == i.EmployeeID).ToList();
                if(getexperience.Count!=0)
                {
                    List<exp> ex = new List<exp>();
                    foreach(var k in getexperience)
                    {
                        exp t = new exp();
                        t.technology = k.technology;
                        t.startdate = (DateTime)k.startdate;
                        t.enddate = (DateTime)k.enddate;
                        ex.Add(t);
                    }
                    em.experience = ex;
                }
                else
                {
                    em.experience = null;
                }
                employeelist.Add(em);
            }

            var a = employeelist.OrderByDescending(x=>x.DOJ).Skip(items * (page - 1)).Take(items);

            if (order == "nameasc")
            {
                a = employeelist.OrderBy(x=>x.EmployeeName).Skip(items * (page - 1)).Take(items);
            }
            else if(order == "namedesc")
            {
                a = employeelist.OrderByDescending(x => x.EmployeeName).Skip(items * (page - 1)).Take(items);
            }
            else if(order == "dateasc")
            {
                a = employeelist.OrderBy(x => x.DOJ).Skip(items * (page - 1)).Take(items);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { a, count});
        }

        public string Post([FromBody]Employee emp)
        {
            try
            {
                Employees dbe = new Employees();
                dbe.EmployeeName = emp.EmployeeName;
                dbe.Department = emp.Department;
                dbe.MailID = emp.MailID;
                dbe.DOJ = emp.DOJ;
                dbe.gender = emp.gender;
                dbe.primaryaddress = emp.primaryaddress;
                dbe.secondaryaddress = emp.secondaryaddress;
                dbe.imgFile = emp.imgFile;

                db.Employees.Add(dbe);
                db.SaveChanges();

                var getid = db.Employees.Where(x => x.MailID == emp.MailID).FirstOrDefault().EmployeeID;

                foreach(var h in emp.hobby)
                {
                    Hobby hb = new Hobby();
                    hb.hobby1 = h;
                    hb.userid = (int)getid;
                    db.Hobby.Add(hb);
                    db.SaveChanges();
                }

                foreach(var ex in emp.experience)
                {
                    UserExperience ux = new UserExperience();
                    ux.employeeid = (int)getid;
                    ux.technology = ex.technology;
                    ux.startdate = ex.startdate;
                    ux.enddate = ex.enddate;
                    db.UserExperience.Add(ux);
                    db.SaveChanges();
                }

                UserDB newuser = new UserDB();

                newuser.name = emp.EmployeeName;
                newuser.email = emp.MailID;
                newuser.password = emp.password;
                newuser.isadmin = false;

                db.UserDB.Add(newuser);
                db.SaveChanges();

                return "Employee Added Successfully";
            }
            catch
            {
                return "Some Error!";
            } 
        }

        public string Put([FromBody] Employee emp)
        {
            try
            {
                var dbe = db.Employees.Where(x => x.EmployeeID == emp.EmployeeID).FirstOrDefault();

                dbe.EmployeeName = emp.EmployeeName;
                dbe.Department = emp.Department;
                dbe.MailID = emp.MailID;
                dbe.DOJ = emp.DOJ;
                dbe.gender = emp.gender;
                dbe.gender = emp.gender;
                dbe.primaryaddress = emp.primaryaddress;
                dbe.secondaryaddress = emp.secondaryaddress;
                dbe.imgFile = emp.imgFile;

                var dbh = db.Hobby.Where(x => x.userid == emp.EmployeeID).ToList();
                var ux = db.UserExperience.Where(x => x.employeeid == emp.EmployeeID).ToList();

                if (dbh.Count != 0)
                {
                    foreach (var i in dbh)
                    {
                        db.Hobby.Remove(i);
                        db.SaveChanges();
                    }
                }
                if (ux.Count != 0)
                {
                    foreach (var j in ux)
                    {
                        db.UserExperience.Remove(j);
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();

                var getid = db.Employees.Where(x => x.MailID == emp.MailID).FirstOrDefault().EmployeeID;

                foreach (var h in emp.hobby)
                {
                    Hobby hb = new Hobby();
                    hb.hobby1 = h;
                    hb.userid = (int)getid;
                    db.Hobby.Add(hb);
                    db.SaveChanges();
                }
                
                foreach (var ex in emp.experience)
                {
                    UserExperience ue = new UserExperience();
                    ue.employeeid = (int)emp.EmployeeID;
                    ue.technology = ex.technology;
                    ue.startdate = ex.startdate;
                    ue.enddate = ex.enddate;
                    db.UserExperience.Add(ue);
                    db.SaveChanges();
                }

                return "Employee Updated Successfully";
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
                var dbe = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
                var dbh = db.Hobby.Where(x => x.userid == id).ToList();

                var ux = db.UserExperience.Where(x => x.employeeid == id).ToList();

                db.Employees.Remove(dbe);
                if (dbh.Count != 0)
                {
                    foreach (var i in dbh)
                    {
                        db.Hobby.Remove(i);
                        db.SaveChanges();
                    }
                }
                if(ux.Count != 0)
                {
                    foreach (var j in ux)
                    {
                        db.UserExperience.Remove(j);
                        db.SaveChanges();
                    }
                }

                return "Employee Deleted Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }

    }
}
