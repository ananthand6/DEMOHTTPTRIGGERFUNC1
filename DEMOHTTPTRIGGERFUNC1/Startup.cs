using System;
using FuncprodAPI.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(FuncprodAPI.Startup))]

namespace FuncprodAPI
{

        public class Startup : FunctionsStartup
        {
            public override void Configure(IFunctionsHostBuilder builder)
            {
                var connStr = Environment.GetEnvironmentVariable("CSTRING");
                builder.Services.AddDbContext<ProductDbContext>(
                  option => option.UseSqlServer(connStr));

                builder.Services.AddHttpClient();
            }
        }
    
}

