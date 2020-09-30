using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuncprodAPI.Models.Data;
using Microsoft.EntityFrameworkCore;


namespace FuncprodAPI.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {
        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Productdetails { get; set; }
        

    }

}

