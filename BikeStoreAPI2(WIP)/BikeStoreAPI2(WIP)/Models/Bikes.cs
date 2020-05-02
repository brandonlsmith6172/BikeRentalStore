using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStoreAPI2_WIP_.Models
{
    public class Bikes
    {
        [Key]
        public int BikeID { get; set; }
        public string Frame { get; set; }
        public string Type { get; set; }
        [ForeignKey("ProductsID")]
        public virtual Products Products { get; set; }
        public int ProductsID { get; set; }        
        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }
        public int BrandID { get; set; }
        public int LocationID { get; set; }


    }
}
