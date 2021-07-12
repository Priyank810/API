using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI;

namespace WebAPI
{
    public class UserAuthentication : IDisposable
    {
        EmployeeDBEntities db = new EmployeeDBEntities();
        public string ValidateUser(string email, string password)
        {
            var getUser = db.UserDB.Where(x => x.email == email && x.password == password).FirstOrDefault();

            if (getUser!=null)
                return "true";
            else
                return "false";
        }
        public void Dispose()
        {
            //Dispose();  
        }

        internal object ValidateUser(object email, string password)
        {
            throw new NotImplementedException();
        }
    }
}