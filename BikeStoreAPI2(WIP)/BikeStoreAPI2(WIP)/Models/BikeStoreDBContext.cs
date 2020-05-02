using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreAPI2_WIP_.Models
{
    public class BikeStoreDBContext : DbContext
    {
        public BikeStoreDBContext(DbContextOptions<BikeStoreDBContext> options)
            : base(options)
        {

        }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
