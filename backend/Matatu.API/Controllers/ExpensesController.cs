using System;
using System.Threading.Tasks;
using Matatu.Domain.Entities;
using Matatu.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matatu.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RecordExpense(Expense expense)
        {
            expense.Id = Guid.NewGuid();
            expense.Timestamp = DateTime.UtcNow;
            
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            
            return Ok(expense);
        }
    }
}
