using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LookUpController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get()
        {
            var result = db.LookUp.ToList();
            List<ShowLookUp> data = new List<ShowLookUp>(); 
            
            if (result.Count != 0)
            {
                foreach(var r in result)
                {
                    ShowLookUp s = new ShowLookUp();

                    s.id = r.id;
                    s.name = r.lookupname;
                    s.description = r.lookupdescription;
                    s.createdby = r.createdby;
                    s.createddate = r.createddate;
                    s.modifieddate = r.modifieddate;
                    s.isActive = r.isActive;

                    data.Add(s);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public HttpResponseMessage Get(string status)
        {
            var isActive = status == "true" ? true : false;

            var result = db.LookUp.Where(x=>x.isActive == isActive).ToList();

            List<ActiveLookUp> data = new List<ActiveLookUp>();

            foreach(var i in result)
            {
                ActiveLookUp activeLookUp = new ActiveLookUp();
                
                activeLookUp.id = i.id;
                activeLookUp.name = i.lookupname;

                data.Add(activeLookUp);

            }
         
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public HttpResponseMessage Get(string value, string id)
        {
            var result = db.LookUp.Where(x => x.lookupname == value).FirstOrDefault();
            var data = "";
            if(result!=null)
            {
                data = "Exist";
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public string Post([FromBody] Lookup lu)
        {
            try
            {

                LookUp lookUp = new LookUp();

                lookUp.lookupname = lu.lookupname;
                lookUp.lookupdescription = lu.lookupdescription;
                lookUp.createdby = lu.createdby;
                lookUp.createddate = DateTime.Now;
                lookUp.isActive = true;

                db.LookUp.Add(lookUp);
                db.SaveChanges();
                
                return "LookUp Added Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
        public string Put([FromBody]  int id)
        {
            try
            {
                var previous = db.LookUp.Where(x => x.id == id).FirstOrDefault();

                previous.isActive = !previous.isActive;

                db.SaveChanges();

                return "LookUp Updated Successfully";
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
                var ld = db.LookUpData.Where(x => x.lookupid == id).ToList();
                foreach (var j in ld)
                {
                    db.LookUpData.Remove(j);
                    db.SaveChanges();
                }

                var l = db.LookUp.Where(x => x.id == id).FirstOrDefault();
                db.LookUp.Remove(l);

                db.SaveChanges();

                return "Employee Deleted Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
    }
}
