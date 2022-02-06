using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PE.ApiHelper.Context;
using PE.BusinessAPIService.Common;
using PE.BusinessAPIService.Common.Calculator;
using PE.BusinessAPIService.Common.Interfaces;
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
        private readonly IBenefitsDeductionCalcRepository _benefitsDeductionCalcRepository;

        public DeductionCalcController(IBenefitsDeductionCalcRepository benefitsDeductionCalcRepository)
        {
            _benefitsDeductionCalcRepository = benefitsDeductionCalcRepository;
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
            BenefitsDeductionResults benefitsDeductionResults = null;
            try
            {
                benefitsDeductionResults = _benefitsDeductionCalcRepository.ReturnBenefitsDeductionCalc(employeeId);
            }
            catch (Exception ex)
            {
                return NotFound(CreatedAtAction("ErrorContext", new { Message = ex.Message, StackTrace = ex.StackTrace }));
            }

            return Ok(benefitsDeductionResults);
        }
    }
}
