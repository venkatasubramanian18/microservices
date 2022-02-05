using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.EmployeeAPIService.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PE.EmployeeAPIService.Common
{
    public class Retrieve:IRetrieve
    {
        private readonly PaylocityContext _context;

        public Retrieve(PaylocityContext context)
        {
            _context = context;
        }

        public IList RetrieveEmployeeData()
        {
            var query = (from Employees in _context.Employees
                        join Dependents in _context.Dependents on Employees.EmployeeId equals Dependents.EmployeeId
                        join DependentTypes in _context.DependentTypes on Dependents.DependentTypeId equals DependentTypes.DependentTypeId
                        join Salaries in _context.Salaries on Employees.EmployeeId equals Salaries.EmployeeId
                        join PaycheckTypes in _context.PaycheckTypes on Salaries.PaycheckTypeId equals PaycheckTypes.PaycheckTypeId
                        select new
                        {
                            EmployeeId = Employees.EmployeeId,
                            EmployeeFirstName = Employees.FirstName,
                            EmployeeLastName = Employees.LastName,
                            DependentTypeId = Dependents.DependentTypeId,
                            DependentFirstName = Dependents.FirstName,
                            DependentFLastName = Dependents.LastName,
                            DependentType = DependentTypes.DependentType,
                            SalaryId = Salaries.SalaryId,
                            EmployeeSalary = Salaries.Salary,
                            EmployeePaycheckType = PaycheckTypes.PaycheckType
                        }).ToList();


            return query;
        }

        public IList RetrieveEmployeeOnlyData()
        {
            using (var context = _context)
            {

                var query = (from employees in _context.Employees
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
                             }).ToList();

                return query;
            }
        }
    }
}
