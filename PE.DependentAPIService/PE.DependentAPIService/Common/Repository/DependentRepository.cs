using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.DependentAPIService.Common.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PE.DependentAPIService.Common.Repository
{
    public class DependentRepository : IDependentRepository
    {
        private readonly PaylocityContext _context;

        public DependentRepository(PaylocityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieve All Dependents data 
        /// </summary>
        /// <returns>Retruns all the dependents</returns>
        public async Task<IList<Dependents>> RetrieveDependentsData()
        {
            var queryDependents = await _context.Dependents
                                .Include(x => x.DependentType)
                                .ToListAsync();

            return queryDependents;
        }

        /// <summary>
        /// Based on the employee Id the counts are measured
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> RetrieveDependentCountById(Guid id)
        {
            var dependents = await _context.Dependents.Where(x => x.EmployeeId == id).ToListAsync();

            return dependents.Count;
        }

        /// <summary>
        /// Dependent types list retrieved from the DB.
        /// Stored in the DB so later we don't want to touch the code
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DependentTypes>> RetrieveDependentTypes()
        {
            return await _context.DependentTypes.ToListAsync();
        }

        /// <summary>
        /// Saves the dependents data into the dependents table
        /// </summary>
        /// <param name="dependents"></param>
        /// <returns></returns>
        public async Task<Dependents> SaveDependents(Dependents dependents)
        {
            dependents.CreatedDate = DateTime.Now;
            _context.Dependents.Add(dependents);

            var returnedCount = await _context.SaveChangesAsync().ConfigureAwait(false);

            if (returnedCount <= 0) dependents = null;

            return dependents;
        }

        /// <summary>
        /// Updates dependents based on the DependentId and changes for the columns passed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dependents"></param>
        /// <returns></returns>
        public async Task UpdatetDependent(Guid id, Dependents dependents)
        {
            _context.Entry(dependents).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            dependents.CreatedDate =
                dependents.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync().ConfigureAwait(false);

        }

        /// <summary>
        /// Deletes the records from the table based on the Dependent Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Dependents> DeleteDependent(Guid id)
        {
            dynamic dependents = null;
            try
            {

                dependents = await _context.Dependents.FindAsync(id);

                _context.Dependents.Remove(dependents);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
               
            }          

            return dependents;
        }

        /// <summary>
        /// Checks if the dependent Exists in the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DependentExists(Guid id)
        {
            return _context.Dependents.Any(e => e.DependentId == id);
        }

    }
}
