using _07_Model_Binding_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _07_Model_Binding_Assignment.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class OrderController : ControllerBase
    {
        //[FromForm] <-- if added [ApiController] attribute above this is needed
        public IActionResult Index([Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))] Order order)
        {
            //if there are any validation errors (as per data annotations)
            if (!ModelState.IsValid)
            {
                string messages = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                //semd HTTP 400 response with error messages
                return BadRequest(messages);
            }

            //generate a random order number between 1 and 99999
            Random random = new Random();
            int randomOrderNumber = random.Next(1, 99999);

            //return HTTP 200 response with order number
            return Ok(new { orderNumber = randomOrderNumber });
        }
    }
}
