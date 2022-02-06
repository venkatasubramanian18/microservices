using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.DependentAPIService.Common.Interfaces;

namespace PE.DependentAPIService.Controllers
{
    /// <summary>
    /// Dependent Api Service responsible for CRUD operation in Database 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DependentsController : ControllerBase
    {
        private readonly IDependentRepository _dependentRepository;

        public DependentsController(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }

        /// <summary>
        /// Get method is to get all the dependents list with it Dependent Type
        /// </summary>
        /// <returns>Returns array of all dependents from DB with dependent types</returns>
        // GET: api/Dependents
        [HttpGet]
        public async Task<ActionResult<IList<Dependents>>> GetDependents()
        {
            var dependentsData = await _dependentRepository.RetrieveDependentsData();

            if (dependentsData == null)
                return NotFound();

            return Ok(dependentsData);
        }

        /// <summary>
        /// Get method is to get all the Dependent Types
        /// </summary>
        /// <returns>Returns array for different Dependent Type</returns>
        // GET: api/Dependents
        [HttpGet("Types")]
        public async Task<ActionResult<IList<DependentTypes>>> GetDependentTypes()
        {
            var dependentTypes = await _dependentRepository.RetrieveDependentTypes();

            if (dependentTypes == null)
                return NotFound();

            return Ok(dependentTypes);
        }

        /// <summary>
        /// Get method is to pull dependents count for a specific Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the Dependents count for a specific Employee</returns>
        // GET: api/Dependents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDependents(Guid id)
        {
            Task<int> dependentsCount = _dependentRepository.RetrieveDependentCountById(id);

            if (dependentsCount.Result == 0)
            {
                return NotFound();
            }

            return Ok(dependentsCount.Result);
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

            try
            {
                await _dependentRepository.UpdatetDependent(id, dependents);
            }
            catch (Exception ex)
            {             
                return NoContent();
            }

            return Ok();
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
            if (dependents == null)
                return BadRequest();

            var savedDependent = await _dependentRepository.SaveDependents(dependents);
            if (savedDependent == null)
                return NoContent();

            return Ok(CreatedAtAction("GetDependents", new { id = dependents.DependentId }, dependents));
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
            var dependents = await _dependentRepository.DeleteDependent(id);

            if (dependents == null)
                return NotFound();

            return Ok();
        }
    }
}
