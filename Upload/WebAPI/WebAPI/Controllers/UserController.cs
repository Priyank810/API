using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();
        public HttpResponseMessage Get()
        {

            var getdata = db.UserDB.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, getdata);

        }
        public HttpResponseMessage Get(string email)
        {

            var i = db.Employees.Where(x => x.MailID == email).FirstOrDefault();

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


        public string Post([FromBody] User user)
        {
            try
            {

                var checkEmail = db.UserDB.Where(x => x.email == user.email).FirstOrDefault();

                if(checkEmail!=null)
                {
                    return "Email Already Exist";
                }

                UserDB newuser = new UserDB();

                newuser.name = user.name;
                newuser.email = user.email;
                newuser.password = user.password;
                newuser.isadmin = true;

                db.UserDB.Add(newuser);
                db.SaveChanges();

                return "User Added Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
    }
}
