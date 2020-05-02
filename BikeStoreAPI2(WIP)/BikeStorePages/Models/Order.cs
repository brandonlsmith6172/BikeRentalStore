using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStorePages.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public string Status { get; set; }

        public string OrderDate { get; set; }
    }
}
