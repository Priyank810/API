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
   
    public class TaskController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get(string date, string eid)
        {

            var d = Convert.ToDateTime(date).AddDays(1);
            var id = (int)db.Employees.Where(x => x.MailID == eid).FirstOrDefault().EmployeeID;
            var gettask = db.Tasks.Where(x=>x.employeeid == id && x.iscompleted!=true).Select(x=>x.tasktitle).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, gettask);
        }

        public HttpResponseMessage Get(string email)
        {

            var eid = (int)db.Employees.Where(x => x.MailID == email).FirstOrDefault().EmployeeID;
            var gettask = db.Tasks.Where(x => x.employeeid == eid).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, gettask);
        }

        public string Post([FromBody] task tk)
        {
            try
            {
                Tasks atk = new Tasks();
                
                atk.employeeid = (int)db.Employees.Where(x => x.MailID == tk.employeemail).FirstOrDefault().EmployeeID;
                atk.tasktitle = tk.tasktitle;
                atk.taskdescription = tk.taskdescription;
                atk.startdate = tk.startdate;
                atk.enddate = tk.enddate;
                atk.iscompleted = tk.iscompleted;

                db.Tasks.Add(atk);
                db.SaveChanges();

                return "Task Added Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
        public string Put([FromBody] task tk)
        {
            try
            {
                var atk = db.Tasks.Where(x => x.taskid == tk.taskid).FirstOrDefault();

                atk.tasktitle = tk.tasktitle;
                atk.taskdescription = tk.taskdescription;
                atk.startdate = tk.startdate;
                atk.enddate = tk.enddate;
                atk.iscompleted = tk.iscompleted;

                db.SaveChanges();

                return "Task Updated Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }

        //[HttpDelete, Route("{id}")]
        public string Delete(long id)
        {
            try
            {
                var atk = db.Tasks.Where(x => x.taskid == id).FirstOrDefault();

                db.Tasks.Remove(atk);

                db.SaveChanges();

                return "Task Removed Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
    }
}
