//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class worklog
    {
        public int logid { get; set; }
        public Nullable<int> taskid { get; set; }
        public string tasktitle { get; set; }
        public string logdescription { get; set; }
        public Nullable<System.DateTime> logdate { get; set; }
        public Nullable<int> loghour { get; set; }
        public Nullable<int> logminute { get; set; }
        public Nullable<int> employeeid { get; set; }
    }
}