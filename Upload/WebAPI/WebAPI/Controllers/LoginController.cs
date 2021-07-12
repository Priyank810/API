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
    public class LoginController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage GET(string email)
        {

            var userData = db.UserDB.Where(x=>x.email == email).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, userData);

        }
    }
}
