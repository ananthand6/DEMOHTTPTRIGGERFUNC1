using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using FuncprodAPI.Models;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using FuncprodAPI.Models.Data;

namespace FuncprodAPI
{
    public class FunctionProducts
    {
        public  ProductDbContext _context;
       

        public FunctionProducts(ProductDbContext context)
        {
            _context = context;
        }
       
        [FunctionName("FunctGetProducts")]
        public IActionResult GetProducts(
           [HttpTrigger(AuthorizationLevel.Anonymous, "GET")] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP GET trigger function processed a request.");

            var productsArray = _context.Productdetails.OrderBy(s => s.ProductId).ToArray();

            return new OkObjectResult(productsArray);

        }

 //******************************************************************************************

        [FunctionName("FunctPostProducts")]    
        public async Task<IActionResult> PostProducts(
           [HttpTrigger(AuthorizationLevel.Anonymous, "POST")] HttpRequest req,
           ILogger log)
        {
            
            /*
            log.LogInformation("C# HTTP POST trigger function processed a request.");

            string reqBody = await new StreamReader(requ.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(reqBody);

            var productsArray2 = _context.Productdetails.Add(data);

            await _context.SaveChangesAsync();

            return new OkObjectResult(productsArray2);
            */

            log.LogInformation("C# HTTP POST trigger function processed a request.");
            
            // read the contents of the posted data into a string
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();

            // use Json.NET to deserialize the posted JSON into a C# dynamic object
            Product data = JsonConvert.DeserializeObject<Product>(reqBody);

            await _context.Productdetails.AddAsync(data);

            await _context.SaveChangesAsync();
            
            return new OkObjectResult(_context.Productdetails.ToArray());

        }

//******************************************************************************************

        [FunctionName("FuncUpdateProducts")]
        public async Task<IActionResult> UpdateProducts(
          [HttpTrigger(AuthorizationLevel.Anonymous, "PUT")] HttpRequest req,
          ILogger log)
        {

            log.LogInformation("C# HTTP PUT trigger function processed a request.");


            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();

            // use Json.NET to deserialize the posted JSON into a C# dynamic object
            Product data = JsonConvert.DeserializeObject<Product>(reqBody);

            var todo = await _context.Productdetails.FindAsync(data.ProductId);
            if (todo == null)
            {
                log.LogWarning($"Item {data.ProductId} not found");
                return new NotFoundResult();
            }

            todo.ProductId = data.ProductId;
            if (data.ProductId != 0)
            {
                todo.ProductName = data.ProductName;
                todo.Quantity = data.Quantity;
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(todo);


        }


    }
}