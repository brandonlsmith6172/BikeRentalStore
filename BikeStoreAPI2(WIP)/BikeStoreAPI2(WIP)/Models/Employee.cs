using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStoreAPI2_WIP_.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
        public int LocationID { get; set; }

    }
}
