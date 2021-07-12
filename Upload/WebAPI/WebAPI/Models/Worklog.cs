using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Worklog
    {
        public int logid { get; set; }
        public string employeemail { get; set; }
        public string tasktitle { get; set; }
        public string logdescription { get; set; }
        public string logdate { get; set; }
        public int loghour { get; set; }
        public int logminute { get; set; }
    }
}