using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;

namespace PE.DependentAPIService.Controllers
{
    /// <summary>
    /// Dependent Api Service responsible for CRUD operation in Database 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DependentsController : ControllerBase
    {
        private readonly PaylocityContext _context;

        public DependentsController(PaylocityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get method is to get all the dependents list with it Dependent Type
        /// </summary>
        /// <returns>Returns array of all dependents from DB with dependent types</returns>
        // GET: api/Dependents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependents>>> GetDependents()
        {
            var queryDependents = await _context.Dependents
                                            .Include(x => x.DependentType)
                                            .ToListAsync();
            return queryDependents;
        }

        /// <summary>
        /// Get method is to get all the Dependent Types
        /// </summary>
        /// <returns>Returns array for different Dependent Type</returns>
        // GET: api/Dependents
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<DependentTypes>>> GetDependentTypes()
        {
            return await _context.DependentTypes.ToListAsync();
        }

        /// <summary>
        /// Get method is to pull dependents count for a specific Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the Dependents count for a specific Employee</returns>
        // GET: api/Dependents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetDependents(Guid id)
        {
            var dependents = await _context.Dependents.Where(x => x.EmployeeId == id).ToListAsync();

            if (dependents == null)
            {
                return NotFound();
            }

            return dependents.Count;
        }

        /// <summary>
        /// Put method is to update the dependents data into the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dependents"></param>
        /// <returns>Returns nothing</returns>
        // PUT: api/Dependents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependents(Guid id, Dependents dependents)
        {
            if (id != dependents.DependentId)
            {
                return BadRequest();
            }

            _context.Entry(dependents).State = EntityState.Modified;

            try
            {
                dependents.CreatedDate = 
                    dependents.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependentsExists(id))
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

        /// <summary>
        /// Post method is to save the requested Dependents data into the Db
        /// </summary>
        /// <param name="dependents"></param>
        /// <returns>Returns created entry for dependents</returns>
        // POST: api/Dependents
        [HttpPost]
        public async Task<ActionResult<Dependents>> PostDependents(Dependents dependents)
        {
            dependents.CreatedDate = DateTime.Now;
            _context.Dependents.Add(dependents);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependents", new { id = dependents.DependentId }, dependents);
        }

        /// <summary>
        /// Delete menthod is to delete the specific dependent provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Dependents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dependents>> DeleteDependents(Guid id)
        {
            var dependents = await _context.Dependents.FindAsync(id);
            if (dependents == null)
            {
                return NotFound();
            }

            _context.Dependents.Remove(dependents);
            await _context.SaveChangesAsync();

            return dependents;
        }

        private bool DependentsExists(Guid id)
        {
            return _context.Dependents.Any(e => e.DependentId == id);
        }
    }
}
