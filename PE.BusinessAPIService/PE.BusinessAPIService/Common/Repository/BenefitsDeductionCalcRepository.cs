using PE.BusinessAPIService.Common.Calculator;
using PE.BusinessAPIService.Common.Interfaces;
using PE.BusinessAPIService.Models;
using System;

namespace PE.BusinessAPIService.Common.Repository
{
    public class BenefitsDeductionCalcRepository : IBenefitsDeductionCalcRepository
    {
        private readonly IBenefitsDeductCalc _benefitsDeductCalc;

        public BenefitsDeductionCalcRepository(IBenefitsDeductCalc benefitsDeductCalc)
        {
            _benefitsDeductCalc = benefitsDeductCalc;
        }

        /// <summary>
        /// The calculation for the benefits deduction and total salary
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Returns a object with all the calculated values for UI report display</returns>
        public BenefitsDeductionResults ReturnBenefitsDeductionCalc(Guid employeeId)
        {
            _benefitsDeductCalc.Initialize(employeeId);

            var employeeBenefitsDeductedPerYear = 
                Math.Round(_benefitsDeductCalc.BenefitsDeductPerYearCalc(true), Constants.ROUNDED_VALUE);
            var employeeBenefitsDeductedPerPayCheck = 
                Math.Round(_benefitsDeductCalc.BenefitsDeductPerPaycheckCalc(true), Constants.ROUNDED_VALUE);

            var dependentsBenefitsDeductedPerYear = 
                Math.Round(_benefitsDeductCalc.BenefitsDeductPerYearCalc(false), Constants.ROUNDED_VALUE);
            var dependentsBenefitsDeductedPerPayCheck = 
                Math.Round(_benefitsDeductCalc.BenefitsDeductPerPaycheckCalc(false), Constants.ROUNDED_VALUE);

            var totalBenefitsDeductedPerPayCheck = 
                Math.Round(employeeBenefitsDeductedPerPayCheck + dependentsBenefitsDeductedPerPayCheck, Constants.ROUNDED_VALUE);
            var totalBenefitsDeductedPerYear = 
                Math.Round(employeeBenefitsDeductedPerYear + dependentsBenefitsDeductedPerYear, Constants.ROUNDED_VALUE);

            var totalSalaryAfterDeducted = 
                Math.Round(_benefitsDeductCalc.EmployeeSalary - totalBenefitsDeductedPerYear, Constants.ROUNDED_VALUE);

            var benefitsDeductionResults = new BenefitsDeductionResults()
            {
                EmployeeBenefitsDeductedPerYear = employeeBenefitsDeductedPerYear,
                EmployeeBenefitsDeductedPerPayCheck = employeeBenefitsDeductedPerPayCheck,
                DependentsBenefitsDeductedPerYear = dependentsBenefitsDeductedPerYear,
                DependentsBenefitsDeductedPerPayCheck = dependentsBenefitsDeductedPerPayCheck,
                TotalBenefitsDeductedPerPayCheck = totalBenefitsDeductedPerPayCheck,
                TotalBenefitsDeductedPerPayYear = totalBenefitsDeductedPerYear,
                TotalSalaryAfterDeducted = totalSalaryAfterDeducted
            };

            return benefitsDeductionResults;
        }
    }
}
