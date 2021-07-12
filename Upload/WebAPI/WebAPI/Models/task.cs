using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class task
    {
        public int taskid {get; set;}
        public string employeemail { get; set; }
        public string tasktitle { get; set; }
        public string taskdescription { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public Boolean iscompleted { get; set; }
    }
}