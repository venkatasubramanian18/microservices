using Moq;
using PE.BusinessAPIService.Common.Interfaces;
using PE.BusinessAPIService.Controllers;
using PE.BusinessAPIService.Models;
using System;
using Xunit;

namespace PE.BusinessAPIService.UnitTests
{
    public class ControllersTests
    {
        private readonly Mock<IBenefitsDeductionCalcRepository> _repository;

        public ControllersTests()
        {
            _repository = new Mock<IBenefitsDeductionCalcRepository>();
        }


        [Fact]
        public void GetDeductionCalc()
        {
            var employeeId = Guid.NewGuid();
            BenefitsDeductionResults benefitsDeductionResults = new BenefitsDeductionResults()
            {
                EmployeeBenefitsDeductedPerPayCheck = decimal.Zero,
                EmployeeBenefitsDeductedPerYear = decimal.Zero,
                DependentsBenefitsDeductedPerPayCheck = decimal.Zero,
                DependentsBenefitsDeductedPerYear = decimal.Zero,
                TotalBenefitsDeductedPerPayCheck = decimal.Zero,
                TotalBenefitsDeductedPerPayYear = decimal.Zero,
                TotalSalaryAfterDeducted = decimal.Zero,
            };

            Mock<IBenefitsDeductionCalcRepository> mockRepo = new Mock<IBenefitsDeductionCalcRepository>();
            mockRepo.Setup(m => m.ReturnBenefitsDeductionCalc(employeeId)).Returns(benefitsDeductionResults);

            var controller = new DeductionCalcController(mockRepo.Object);

            var actualResult = controller.DeductionCalc(employeeId);
            
            Assert.NotNull(actualResult);

        }
    }
}
