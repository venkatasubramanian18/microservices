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

        //TODO: Need to change PaycheckType in database instead of converting here
        private decimal PayCheckPerAnnum =>
            Convert.ToDecimal(
                _context.PaycheckTypes
                    .Where(pct => 
                        pct.PaycheckTypeId == employeeData.Salaries.First().PaycheckTypeId).First().PaycheckType
            );                    

        public void Initialize(Guid employeeId)
        {
            _employeeId = employeeId;
            employeeData = _context.Employees
                                .Where(x => x.EmployeeId == _employeeId)
                                .Include(x => x.Dependents)
                                .Include(x => x.Salaries).FirstOrDefault();
        }

        public decimal EmployeeSalary =>
            Convert.ToDecimal(employeeData.Salaries.First().Salary);

        public decimal BenefitsDeductPerPaycheckCalc(bool isEmployee)
        {
            return BenefitsDeductPerYearCalc(isEmployee) / PayCheckPerAnnum;
        }

        public decimal BenefitsDeductPerYearCalc(bool isEmployee)
        {
            return 
                isEmployee 
                ? EmployeeBenefitsDeductPerYear() 
                : DependentsBenefitsDeductPerYear();
        }

        public decimal EmployeeBenefitsDeductPerYear() =>       
            Constants.EMPLOYEE_BENEFITS_PER_YEAR * (1 - _nameBasedDiscount.Discount(employeeData.FirstName));

        public decimal DependentsBenefitsDeductPerYear() =>
            employeeData.Dependents.Any()
                ? employeeData.Dependents.Sum(d => Constants.DEPENDENT_BENEFITS_PER_YEAR * (1 - _nameBasedDiscount.Discount(d.FirstName)))
                : 0;
    }
}
