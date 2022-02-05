using System;

namespace PE.BusinessAPIService.Common.Calculator
{
    public interface IBenefitsDeductCalc
    {
        public void Initialize(Guid employeeId);
        public decimal BenefitsDeductPerPaycheckCalc(bool isEmployee);
        public decimal BenefitsDeductPerYearCalc(bool isEmployee);
        public decimal CalculatedDedecutedSalay();

    }
}
