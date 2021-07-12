using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class WorklogController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();
        public HttpResponseMessage Get()
        {
            var getlogs = db.worklog.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, getlogs);
        }
        public HttpResponseMessage Get(string date, string eid)
        {

            var d = Convert.ToDateTime(date);
            var id = (int)db.Employees.Where(x => x.MailID == eid).FirstOrDefault().EmployeeID;
            var gettask = db.worklog.Where(x => x.employeeid == id && d==x.logdate).ToList();
            var count = 0;
            if (gettask.Count != 0)
            {
                foreach (var i in gettask)
                {
                    count += (int)(i.loghour * 60 + i.logminute);
                }
            }
            var h = count / 60;
            var m = count - h * 60;
            return Request.CreateResponse(HttpStatusCode.OK, new { gettask, h, m });
        }

        public string Post([FromBody] Worklog wl)
        {
            try
            {
                worklog log = new worklog();

                log.taskid = db.Tasks.Where(x => x.tasktitle == wl.tasktitle).FirstOrDefault().taskid;
                log.tasktitle = wl.tasktitle;
                log.employeeid = (int)db.Employees.Where(x => x.MailID == wl.employeemail).FirstOrDefault().EmployeeID;
                log.logdescription = wl.logdescription;
                log.logdate = Convert.ToDateTime(wl.logdate);
                log.loghour = wl.loghour;
                log.logminute = wl.logminute;

                db.worklog.Add(log);
                db.SaveChanges();

                return "Log Added Successfully";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string Put([FromBody] Worklog wl)
        {
            try
            {
                var log = db.worklog.Where(x => x.logid == wl.logid).FirstOrDefault();

                log.taskid = db.Tasks.Where(x=>x.tasktitle == wl.tasktitle).FirstOrDefault().taskid;
                log.tasktitle = wl.tasktitle;
                log.employeeid = (int)db.Employees.Where(x => x.MailID == wl.employeemail).FirstOrDefault().EmployeeID;
                log.logdescription = wl.logdescription;
                log.logdate = Convert.ToDateTime(wl.logdate);
                log.loghour = wl.loghour;
                log.logminute = wl.logminute;

                db.SaveChanges();

                return "Log Updated Successfully";
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
                var log = db.worklog.Where(x => x.logid == id).FirstOrDefault();

                db.worklog.Remove(log);
                db.SaveChanges();

                return "Log Removed Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
    }
}
