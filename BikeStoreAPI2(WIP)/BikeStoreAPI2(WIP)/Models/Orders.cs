using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BikeStoreAPI2_WIP_.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("OrderDetailsID")]
        public OrderDetails OrderDetails { get; set; }
        public int OrderDetailsID { get; set; }
       
        public int CustomerID { get; set; }
       
        public int LocationID { get; set; }
        public int ProductsID { get; set; }
       
        public int EmployeeID { get; set; }
        public string OrderStatus { get; set; }
        public System.DateTime OrderDate { get; set; }

    }
}
