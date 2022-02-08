using Moq;
using PE.BusinessAPIService.Common;
using PE.BusinessAPIService.Common.CalcBenefitsDiscount;
using PE.BusinessAPIService.Common.Calculator;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PE.BusinessAPIService.UnitTests
{
    public class CommonTests
    {
        private readonly Mock<IBenefitsDeductCalc> _mockBenefitsDeductCalc;

        public CommonTests()
        {
            _mockBenefitsDeductCalc = new Mock<IBenefitsDeductCalc>();
        }

        [Fact]
        public void Discount_GetZeroDiscount()
        {
            string name = "Venkat";
            NameBasedDiscount nameBasedDiscount = new NameBasedDiscount();
            var result = nameBasedDiscount.Discount(name);

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Discount_GetValidDiscount()
        {
            string name = "A_Venkat";
            NameBasedDiscount nameBasedDiscount = new NameBasedDiscount();
            var result = nameBasedDiscount.Discount(name);

            Assert.Equal(result, Constants.NAME_STARTS_WITH_A_DISCOUNT);
        }
    }
}
