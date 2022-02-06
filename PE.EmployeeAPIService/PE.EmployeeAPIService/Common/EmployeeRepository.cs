using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.EmployeeAPIService.Common.Interface;
using PE.EmployeeAPIService.Models;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PE.EmployeeAPIService.Common
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly PaylocityContext _context;
        private bool _disposed;

        public EmployeeRepository(PaylocityContext context)
        {
            _context = context;
        }

        public async Task<List<PaycheckTypes>> RetrieveAllPaycheckTypes()
        {
            return await _context.PaycheckTypes.ToListAsync();
        }

        public async Task<IList> RetrieveEmployeeOnlyData()
        {

            var query = await (from employees in _context.Employees
                               from salaries in _context.Salaries.Where(x => x.EmployeeId == employees.EmployeeId).DefaultIfEmpty()
                               from paychecktypes in _context.PaycheckTypes.Where(x => x.PaycheckTypeId == salaries.PaycheckTypeId).DefaultIfEmpty()
                               select new
                               {
                                   EmployeeId = employees.EmployeeId,
                                   FirstName = employees.FirstName,
                                   LastName = employees.LastName,
                                   SalaryId = salaries.SalaryId,
                                   Salary = salaries.Salary,
                                   PaycheckTypeId = paychecktypes.PaycheckTypeId,
                                   PaycheckType = paychecktypes.PaycheckType
                               }).ToListAsync();

            return query;

        }

        public async Task<Employees> RetrieveEmployeeById(Guid employeeId)
        {
            return await _context.Employees.Where(emp => emp.EmployeeId == employeeId).FirstAsync();
        }

        public async Task<Employees> SaveEmployee(UpdateEmployees updateEmployees)
        {
            Employees employees = null;
            var returnedCount = 0;
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

                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    var paycheckID = _context.PaycheckTypes.Where(x => x.PaycheckType == updateEmployees.PaycheckType).Select(y => y.PaycheckTypeId).FirstOrDefault();
                    if (paycheckID == null)
                        throw new Exception();

                    Salaries salaries = new Salaries() { EmployeeId = employees.EmployeeId, Salary = updateEmployees.Salary, SalaryId = new Guid(), PaycheckTypeId = paycheckID };
                    _context.Salaries.Add(salaries);

                    returnedCount = await _context.SaveChangesAsync().ConfigureAwait(false);
                    context.Commit();
                }
                catch (Exception ex)
                {
                    Log.Error("Execution for Insert employee failed. " + "Message : " + ex.Message + "Stacktrace : " + ex.StackTrace);
                    context.Rollback();
                }

                if (returnedCount <= 0) employees = null;

                return employees;
            }
        }

        public async Task UpdateEmployee(Guid id, UpdateEmployees updateEmployees)
        {
            Employees employees = new Employees()
            {
                EmployeeId = updateEmployees.EmployeeId,
                FirstName = updateEmployees.FirstName,
                LastName = updateEmployees.LastName,
                CreatedDate = updateEmployees.CreatedDate,
                ModifiedDate = DateTime.Now
            };

            employees.ModifiedDate =
                employees.CreatedDate = DateTime.Now;

            _context.Entry(employees).State = EntityState.Modified;

            var salaries = _context.Salaries.FirstOrDefault(x => x.EmployeeId == employees.EmployeeId);
            salaries.Salary = updateEmployees.Salary;
            employees.Salaries.Add(salaries);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Employees> DeleteEmployee(Guid employeeId)
        {
            dynamic employees = null;
            using (var context = _context.Database.BeginTransaction())
            {
                try
                {
                    employees = await _context.Employees.FindAsync(employeeId);
                    if (employees == null)
                    {
                        return null;
                    }

                    var salary = await _context.Salaries.Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
                    if (salary != null)
                        _context.Salaries.Remove(salary);

                    var dependents = _context.Dependents?.Where(y => y.EmployeeId == employeeId);
                    if (dependents != null)
                        _context.Dependents.RemoveRange(_context.Dependents?.Where(y => y.EmployeeId == employeeId));

                    _context.Employees.Remove(employees);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    await context.CommitAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Log.Error("Execution for Delete employee failed. " + "Message : " + ex.Message + "Stacktrace : " + ex.StackTrace);
                    await context.RollbackAsync().ConfigureAwait(false);
                }
            }

            return employees;
        }
    }
}
