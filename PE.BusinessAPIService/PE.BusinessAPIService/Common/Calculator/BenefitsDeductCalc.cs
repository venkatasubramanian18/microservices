using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.BusinessAPIService.Common.CalcBenefitsDiscount;
using PE.BusinessAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static PE.BusinessAPIService.Common.Constants;

namespace PE.BusinessAPIService.Common.Calculator
{
    public class BenefitsDeductCalc : IBenefitsDeductCalc
    {
        private readonly PaylocityContext _context;
        private readonly INameBasedDiscount _nameBasedDiscount;
        private Guid _employeeId { get; set; }
        internal static Employees employeeData { get; set; }

        public BenefitsDeductCalc(PaylocityContext context, INameBasedDiscount nameBasedDiscount)
        {
            _context = context;
            _nameBasedDiscount = nameBasedDiscount;

        }

        /// <summary>
        /// Query to fetch the paycheck type chose per annum
        /// </summary>
        //TODO: Need to change PaycheckType in database instead of converting here
        private decimal PayCheckPerAnnum =>
            Convert.ToDecimal(
                _context.PaycheckTypes
                    .Where(pct => 
                        pct.PaycheckTypeId == employeeData.Salaries.First().PaycheckTypeId).First().PaycheckType
            );                    

        /// <summary>
        /// One time initializization the employeeId from the repository so we don't want to overload for all methods
        /// </summary>
        /// <param name="employeeId"></param>
        public void Initialize(Guid employeeId)
        {
            _employeeId = employeeId;
            employeeData = _context.Employees
                                .Where(x => x.EmployeeId == _employeeId)
                                .Include(x => x.Dependents)
                                .Include(x => x.Salaries).FirstOrDefault();
        }

        /// <summary>
        /// Gets the Employee Salary
        /// </summary>
        public decimal EmployeeSalary =>
            Convert.ToDecimal(employeeData.Salaries.First().Salary);

        /// <summary>
        /// Calculation for benefits deducted per pay check
        /// isEmployee is to differentiate between employee and dependents
        /// </summary>
        /// <param name="isEmployee"></param>
        /// <returns></returns>
        public decimal BenefitsDeductPerPaycheckCalc(bool isEmployee)
        {
            return BenefitsDeductPerYearCalc(isEmployee) / PayCheckPerAnnum;
        }

        /// <summary>
        /// Based on if employee or dependents the benefits deducted are calculated per year
        /// </summary>
        /// <param name="isEmployee"></param>
        /// <returns></returns>
        public decimal BenefitsDeductPerYearCalc(bool isEmployee)
        {
            return 
                isEmployee 
                ? EmployeeBenefitsDeductPerYear() 
                : DependentsBenefitsDeductPerYear();
        }

        /// <summary>
        /// Employee benefits deduction calculation based on names
        /// </summary>
        /// <returns></returns>
        public decimal EmployeeBenefitsDeductPerYear() =>       
            Constants.EMPLOYEE_BENEFITS_PER_YEAR * (1 - _nameBasedDiscount.Discount(employeeData.FirstName));

        /// <summary>
        /// Dependent benefits deduction calculation based on names
        /// </summary>
        /// <returns></returns>
        public decimal DependentsBenefitsDeductPerYear() =>
            employeeData.Dependents.Any()
                ? employeeData.Dependents.Sum(d => Constants.DEPENDENT_BENEFITS_PER_YEAR * (1 - _nameBasedDiscount.Discount(d.FirstName)))
                : 0;
    }
}
