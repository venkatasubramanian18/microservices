using PE.BusinessAPIService.Models;
using System;

namespace PE.BusinessAPIService.Common.Interfaces
{
    public interface IBenefitsDeductionCalcRepository
    {
        BenefitsDeductionResults ReturnBenefitsDeductionCalc(Guid employeeId);
    }
}
