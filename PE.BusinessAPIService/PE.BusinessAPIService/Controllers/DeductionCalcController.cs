using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PE.ApiHelper.Context;
using PE.BusinessAPIService.Common;
using PE.BusinessAPIService.Common.Calculator;
using PE.BusinessAPIService.Models;
using System;
using System.Threading.Tasks;

namespace PE.BusinessAPIService.Controllers
{
    /// <summary>
    /// Business Api service is used to calculate the deductiion and salary for report creation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionCalcController : ControllerBase
    {
        private readonly IBenefitsDeductCalc _benefitsDeductCalc;

        public DeductionCalcController(IBenefitsDeductCalc benefitsDeductCalc)
        {
            _benefitsDeductCalc = benefitsDeductCalc;
        }

        /// <summary>
        /// Calculate benefites deducted and salary for a specific Employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Returns data for report</returns>
        // GET api/<DeductionCalcController>
        [HttpGet("{employeeId}")]
        public ActionResult<BenefitsDeductionResults> DeductionCalc(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
                return BadRequest();

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

            return Ok(benefitsDeductionResults);
        }
    }
}
