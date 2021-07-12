using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class exp
    {
        public string technology { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
    public class Employee
    {
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string MailID { get; set; }
        public DateTime? DOJ { get; set; }
        public string gender { get; set; }
        public List<string> hobby { get; set; }
        public string primaryaddress { get; set; }
        public string secondaryaddress { get; set; }
        public string imgFile { get; set; }
        public List<exp> experience { get; set; }
        public string password { get; set; }
    }
}