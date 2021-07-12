using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LookUpValuesController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get(string id)
        {
            var i = Int32.Parse(id);
            var data = db.LookUpData.Where(x => x.lookupid == i).Select(x => x.value).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public string Post([FromBody] Lookupdata lookupdata)
        {
            try
            {
                foreach (var i in lookupdata.items)
                {
                    LookUpData ld = new LookUpData();

                    ld.lookupid = lookupdata.lookupid;
                    ld.value = i;
                    ld.valueindex = 0;

                    db.LookUpData.Add(ld);
                    db.SaveChanges();
                }

                return "LookUpData Added Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
        public string Put([FromBody] Lookupdata lookupdata)
        {
            try
            {
                var previous = db.LookUpData.Where(x => x.lookupid == lookupdata.lookupid).ToList();
                
                foreach(var j in previous)
                {
                    db.LookUpData.Remove(j);
                    db.SaveChanges();
                }

                foreach (var i in lookupdata.items)
                {
                    LookUpData ld = new LookUpData();

                    ld.lookupid = lookupdata.lookupid;
                    ld.value = i;
                    ld.valueindex = 0;

                    db.LookUpData.Add(ld);
                    db.SaveChanges();
                }

                return "LookUpData Updated Successfully";
            }
            catch
            {
                return "Some Error!";
            }
        }
    }
}
