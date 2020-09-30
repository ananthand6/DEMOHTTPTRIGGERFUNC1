using System;
using FuncprodAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace FuncprodAPI
{

    public class ProductFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("CSTRING"));

            return new ProductDbContext(optionsBuilder.Options);
        }
    }

}
