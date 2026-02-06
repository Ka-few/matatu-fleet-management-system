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
    public class CollectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RecordCollection(Collection collection)
        {
            collection.Id = Guid.NewGuid();
            collection.Timestamp = DateTime.UtcNow;
            
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
            
            return Ok(collection);
        }
    }
}
