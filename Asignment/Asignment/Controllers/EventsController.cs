using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asignment.Data;
using Asignment.Models;

namespace Asignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AsignmentContext _context;

        public EventsController(AsignmentContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<OkObjectResult> GetEvent(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? orderColumn = null,
            [FromQuery] string? orderDir = null)
        {
            var list = await _context.Event.ToListAsync();
            var query = list.AsQueryable(); // móc từ db.
            if (!string.IsNullOrEmpty(search))
            {
                // search theo keyword
                query = query.Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(orderColumn) && !string.IsNullOrEmpty(orderDir))
            {
                // sort by field
                query = query.OrderBy($"{orderColumn} {orderDir}");
            }
            var totalCount = query.Count();
            var pagedProducts = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new
            {
                data = pagedProducts,
                recordsTotal = list.Count,
                recordsFiltered = totalCount,
                page = page,
                pageSize = pageSize
            };
            return Ok(result);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(long id)
        {
            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(long id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(long id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
