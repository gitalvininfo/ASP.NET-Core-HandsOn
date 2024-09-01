using Microsoft.AspNetCore.Mvc;

namespace _06_Controllers_Assignment.Controllers
{

    public class BankController : Controller
    {

        [Route("")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank");
        }


        [Route("account-details")]
        public IActionResult GetAccountDetails()
        {
            return new JsonResult(
                new { accountNumber = 1001, accountHolderName = "Example Name", currentBalance = 5000 });
        }


        [Route("account-statement")]
        public IActionResult GetAccountStatement()
        {
            return File("assignment.html", "text/html");
        }

        [Route("get-current-balance/{accountNumber:int?}")]
        public IActionResult GetCurrentBalance()
        {
            object accountNumberObj;

            // get the accountNumber from route parameters
            if (HttpContext.Request.RouteValues
                .TryGetValue("accountNumber", out accountNumberObj) 
                && accountNumberObj is string accountNumber)
            {

                // check if account number is supplied
                if (string.IsNullOrEmpty(accountNumber))
                {
                    return NotFound("Account number should be supplied");
                }

                if (int.TryParse(accountNumber, out int accountNumberInt))
                {
                    var bankAccount = new { accountNumber = 1001, accountHolderName = "Example Name", currentBalance = 5000 };

                    if(accountNumberInt == 1001)
                    {
                        return Content(bankAccount.currentBalance.ToString());
                    }
                    else
                    {
                        return BadRequest("Account number should be 1001");
                    }
                }
                else
                {
                    return BadRequest("Invalid account number format");
                }

            }
            else
            {
                return NotFound("Account number should be supplied");
            }

        }



    }
}
