using System;
using System.Linq;
using System.Threading.Tasks;
using Matatu.Domain.Entities;
using Matatu.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Matatu.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await _context.Vehicles.Include(v => v.Owner).ToListAsync();
            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            // Minimal validation for MVP
            vehicle.Id = Guid.NewGuid();
            vehicle.CreatedAt = DateTime.UtcNow;
            
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetVehicles), new { id = vehicle.Id }, vehicle);
        }
    }
}
