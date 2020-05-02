using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStoreAPI2_WIP_.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }
        public int BrandID { get; set; }

        [ForeignKey("LocationID")]
        public Location Location { get; set; }
        public int LocationID { get; set; }
        public decimal RentalPrice { get; set; }
        
    }
}
