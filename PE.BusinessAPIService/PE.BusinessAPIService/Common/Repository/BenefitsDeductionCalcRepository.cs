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

        public BenefitsDeductionResults ReturnBenefitsDeductionCalc(Guid employeeId)
        {
            _benefitsDeductCalc.Initialize(employeeId);

            var employeeBenefitsDeductedPerYear = Math.Round(_benefitsDeductCalc.BenefitsDeductPerYearCalc(true), 2);
            var employeeBenefitsDeductedPerPayCheck = Math.Round(_benefitsDeductCalc.BenefitsDeductPerPaycheckCalc(true), 2);

            var dependentsBenefitsDeductedPerYear = Math.Round(_benefitsDeductCalc.BenefitsDeductPerYearCalc(false), 2);
            var dependentsBenefitsDeductedPerPayCheck = Math.Round(_benefitsDeductCalc.BenefitsDeductPerPaycheckCalc(false), 2);

            var totalBenefitsDeductedPerPayCheck = Math.Round(employeeBenefitsDeductedPerPayCheck + dependentsBenefitsDeductedPerPayCheck, 2);
            var totalBenefitsDeductedPerYear = Math.Round(employeeBenefitsDeductedPerYear + dependentsBenefitsDeductedPerYear, 2);

            var totalSalaryAfterDeducted = Math.Round(_benefitsDeductCalc.CalculatedDedecutedSalay() - totalBenefitsDeductedPerYear, 2);

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
