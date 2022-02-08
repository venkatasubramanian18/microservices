using System;

namespace PE.BusinessAPIService.Common.Calculator
{
    public interface IBenefitsDeductCalc
    {
        void Initialize(Guid employeeId);
        decimal BenefitsDeductPerPaycheckCalc(bool isEmployee);
        decimal BenefitsDeductPerYearCalc(bool isEmployee);
        decimal CalculatedDedecutedSalay();

    }
}
