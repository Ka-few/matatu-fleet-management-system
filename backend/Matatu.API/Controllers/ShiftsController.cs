using System;
using System.Threading.Tasks;
using Matatu.Domain.Entities;
using Matatu.Domain.Enums;
using Matatu.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Matatu.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartShift(Shift shift)
        {
            shift.Id = Guid.NewGuid();
            shift.StartTime = DateTime.UtcNow;
            shift.Status = ShiftStatus.Active;
            
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            
            return Ok(shift);
        }

        [HttpPost("{id}/end")]
        public async Task<IActionResult> EndShift(Guid id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return NotFound();

            shift.EndTime = DateTime.UtcNow;
            shift.Status = ShiftStatus.Closed;
            
            await _context.SaveChangesAsync();
            return Ok(shift);
        }
    }
}
