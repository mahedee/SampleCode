using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class Employee
    {
        public Int64 Gid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DeptId { get; set; }
        public int DesignationId { get; set; }
        public Int16 BloodGroupId { get; set; }
        public Int16 MaritalStatus { get; set; }
        public DateTime JoiningDt { get; set; }
    }
}