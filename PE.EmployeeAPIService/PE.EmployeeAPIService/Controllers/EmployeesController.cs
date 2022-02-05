using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE.EmployeeAPIService.Models;
using PE.EmployeeAPIService.Common;
using System.Collections;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using Serilog;

namespace PE.EmployeeAPIService.Controllers
{
    /// <summary>
    /// Employee Api Service responsible for CRUD operation in Database 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly PaylocityContext _context;
        private readonly IRetrieve _retrieve;

        public EmployeesController(PaylocityContext context, IRetrieve retrieve)
        {
            _context = context;
            _retrieve = retrieve;
        }

        /// <summary>
        /// Get method returns Employee only data
        /// </summary>
        /// <returns>Returns arrays of Employee data</returns>
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            Log.Information("Executing Get method returns Employee only data");
            throw new NotImplementedException();
            var query = _retrieve.RetrieveEmployeeOnlyData();
            //return await _context.Employees.ToListAsync();
            return Ok(query);
        }

        /// <summary>
        /// Get method returns Paycheck Types
        /// </summary>
        /// <returns>Returns arrays of Paycheck Types</returns>
        // GET: api/PaycheckTypes
        [HttpGet("PaycheckTypes")]
        public async Task<ActionResult<IEnumerable<PaycheckTypes>>> GetPaycheckTypes()
        {            
            return Ok(await _context.PaycheckTypes.ToListAsync());
        }

        /// <summary>
        /// Get method returns Employee Data using Employee Id
        /// </summary>
        /// <returns>Returns Employee data specifc to an Employee</returns>
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployees(Guid id)
        {
            var employees = await _context.Employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        /// <summary>
        /// Put method updates the Employee data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateEmployees"></param>
        /// <returns>Returns nothing</returns>
        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees(Guid id, UpdateEmployees updateEmployees)
        {

            Employees employees = new Employees()
            {
                EmployeeId = updateEmployees.EmployeeId,
                FirstName = updateEmployees.FirstName,
                LastName = updateEmployees.LastName,
                CreatedDate = updateEmployees.CreatedDate,
                ModifiedDate = DateTime.Now
            };

            if (id != employees.EmployeeId)
            {
                return BadRequest();
            }

            employees.ModifiedDate = 
                employees.CreatedDate = DateTime.Now;

            _context.Entry(employees).State = EntityState.Modified;

            try
            {
                var salaries = _context.Salaries.FirstOrDefault(x => x.EmployeeId == employees.EmployeeId);
                salaries.Salary = updateEmployees.Salary;
                employees.Salaries.Add(salaries);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
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
        /// Post method updates the Employee data into database
        /// </summary>
        /// <param name="updateEmployees"></param>
        /// <returns>On creation it returns the created Employee details</returns>
        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployees(UpdateEmployees updateEmployees)
        {
            Employees employees = null;
            using (var context = _context.Database.BeginTransaction())
            {
                try
                {
                    employees = new Employees()
                    {
                        EmployeeId = Guid.NewGuid(),
                        FirstName = updateEmployees.FirstName,
                        LastName = updateEmployees.LastName,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.MinValue
                    };

                    _context.Employees.Add(employees);

                    await _context.SaveChangesAsync();

                    var paycheckID = _context.PaycheckTypes.Where(x => x.PaycheckType == updateEmployees.PaycheckType).Select(y => y.PaycheckTypeId).FirstOrDefault();
                    if (paycheckID == null)
                        throw new Exception();

                    Salaries salaries = new Salaries() { EmployeeId = employees.EmployeeId, Salary = updateEmployees.Salary, SalaryId = new Guid(), PaycheckTypeId = paycheckID };
                    _context.Salaries.Add(salaries);

                    await _context.SaveChangesAsync();
                    context.Commit();
                }
                catch (Exception)
                {
                    context.Rollback();
                }

                return Ok(CreatedAtAction("GetEmployees", new { id = employees.EmployeeId }, employees));
            }
        }

        /// <summary>
        /// Delete method delete a particular Employee data from all its foreign key related tables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employees>> DeleteEmployees(Guid id)
        {
            dynamic employees = null;
            using (var context = _context.Database.BeginTransaction())
            {
                try
                {
                    employees = await _context.Employees.FindAsync(id);
                    if (employees == null)
                    {
                        return NotFound();
                    }

                    var salary = await _context.Salaries.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
                    if (salary != null)
                        _context.Salaries.Remove(salary);

                    var dependents = _context.Dependents?.Where(y => y.EmployeeId == id);
                    if (dependents != null)
                        _context.Dependents.RemoveRange(_context.Dependents?.Where(y => y.EmployeeId == id));

                    _context.Employees.Remove(employees);
                    await _context.SaveChangesAsync();

                    await context.CommitAsync();
                }
                catch (Exception ex)
                {
                    await context.RollbackAsync();
                }
             }

            return employees;
        }

        private bool EmployeesExists(Guid id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
