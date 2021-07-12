using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Lookupdata
    {
        public int lookupid { get; set; }
        public string[] items { get; set; }
    }
    public class Lookup
    {
        public int? lookupid { get; set; }
        public string lookupname { get; set; }
        public string lookupdescription { get; set; }
        public string createdby { get; set; }
        public string active { get; set; }
        public DateTime createddate { get; set; }
        public DateTime? modifieddate { get; set; }

    }

    public class ActiveLookUp
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class ShowLookUp
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string createdby { get; set; }
        public Boolean? isActive { get; set; }
        public DateTime? createddate { get; set; }
        public DateTime? modifieddate { get; set; }
    }
}