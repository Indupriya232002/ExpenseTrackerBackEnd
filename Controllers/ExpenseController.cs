using BusinessLayer.Services;
using EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [Authorize]
        [Route("AddExpense")]
        [HttpPost]
        public IActionResult AddExpense(Expenses expenses)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdClaim);
            expenses.userId = userId;
            var result = _expenseService.AddExpense(expenses);
            return Ok(result);

        }

        [Route("GetAllExpenses")]
        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            var result = _expenseService.GetAllExpenses();
            return Ok(result);
        }

        [Authorize]
        [Route("GetUserExpenses")]
        [HttpGet]
        public IActionResult GetUserExpenses()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Retrieve user ID from JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdClaim);
            var result = _expenseService.GetExpenseByUserId(userId);
            return Ok(result);
        }

        [Authorize]
        [Route("UpdateExpenses")]
        [HttpPut]
        public IActionResult UpdateExpenses(Expenses expenses)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdClaim);
            expenses.userId = userId;
            var result = _expenseService.UpdateExpenses(expenses);
            return Ok(result);
        }

        [Route("DeleteExpenseById")]
        [HttpDelete]
        public IActionResult DeleteExpenseById(int expenseId)
        {
            var result = _expenseService.DeleteExpenseById(expenseId);
            return Ok(result);
        }

        [Authorize]
        [Route("DeleteExpenseByUserIdAndExpenseId")]
        [HttpDelete]
        public IActionResult DeleteExpenseByUserIdAndExpenseId(int userId, int expenseId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || int.Parse(userIdClaim) != userId)
            {
                return Unauthorized("User ID not found in token or does not match.");
            }

            // Check if the expense belongs to the user
            var result = _expenseService.DeleteExpenseByUserIdAndExpenseId(userId, expenseId);

            return result ? Ok(new { message = "Expense Deleted Successfully" }) : NotFound(new { message = "Expense Not Found or Unauthorized Access" });

        }

        [HttpGet("GetDailyExpenses")]
        public IActionResult GetDailyExpenses()
        {
            var expenses = _expenseService.GetDailyExpenses();

            // Calculate total expenditure for today
            var totalExpense = expenses.Sum(e => e.expenditure);

            return Ok(new
            {
                TotalAmount = totalExpense,
                Expenses = expenses
            });
        }


        [HttpGet("GetExpensesByDateRange")]
        public IActionResult GetExpensesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {

                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim);

            var expenses = _expenseService.GetExpensesByDateRange(startDate, endDate, userId);

            return Ok(expenses);
        }

    }
}
