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
using PE.EmployeeAPIService.Common.Interface;

namespace PE.EmployeeAPIService.Controllers
{
    /// <summary>
    /// Employee Api Service responsible for CRUD operation in Database 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Get method returns Employee only data
        /// </summary>
        /// <returns>Returns arrays of Employee data</returns>
        // GET: api/Employees
        [HttpGet]
        public async Task<IList> GetEmployees()
        {
            Log.Information("Executing Get method returns Employee only data");
            return await _employeeRepository.RetrieveEmployeeOnlyData(); 
        }

        /// <summary>
        /// Get method returns Paycheck Types
        /// </summary>
        /// <returns>Returns arrays of Paycheck Types</returns>
        // GET: api/PaycheckTypes
        [HttpGet("PaycheckTypes")]
        public async Task<ActionResult<List<PaycheckTypes>>> GetPaycheckTypes()
        {            
            return await _employeeRepository.RetrieveAllPaycheckTypes();
        }

        /// <summary>
        /// Get method returns Employee Data using Employee Id
        /// </summary>
        /// <returns>Returns Employee data specifc to an Employee</returns>
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployees(Guid id)
        {
            var employee = await _employeeRepository.RetrieveEmployeeById(id);

            if (employee.EmployeeId == Guid.Empty)
            {
                return NotFound();
            }

            return Ok(employee);
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

            if (id != updateEmployees.EmployeeId)
                return BadRequest();

            try
            {
                await _employeeRepository.UpdateEmployee(id, updateEmployees);
            }
            catch (Exception ex)
            {
                Log.Error("Execution for update employee failed. " + "Message : " + ex.Message + "Stacktrace : " + ex.StackTrace);
                return NoContent();
            }

            return Ok();
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
            if (updateEmployees == null)
                return BadRequest();

            var employee = await _employeeRepository.SaveEmployee(updateEmployees);
            return Ok(CreatedAtAction("PostEmployees", new { id = employee?.EmployeeId }));            
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
            var employee = await _employeeRepository.DeleteEmployee(id);
            if(employee == null)
                return NotFound();

            return Ok();
        }
    }
}
