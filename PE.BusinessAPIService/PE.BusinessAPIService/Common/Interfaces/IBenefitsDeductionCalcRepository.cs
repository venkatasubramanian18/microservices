using PE.BusinessAPIService.Models;
using System;

namespace PE.BusinessAPIService.Common.Interfaces
{
    public interface IBenefitsDeductionCalcRepository
    {
        public BenefitsDeductionResults ReturnBenefitsDeductionCalc(Guid employeeId);
    }
}
