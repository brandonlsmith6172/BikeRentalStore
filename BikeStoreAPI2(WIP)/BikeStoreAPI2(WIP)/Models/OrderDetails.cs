using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStoreAPI2_WIP_.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsID {get;set;}
        public decimal TotalPrice { get; set; }
    }
}
