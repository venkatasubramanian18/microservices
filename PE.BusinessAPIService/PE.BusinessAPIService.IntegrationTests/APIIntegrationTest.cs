using Newtonsoft.Json;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.BusinessAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PE.BusinessAPIService.IntegrationTests
{
    public class APIIntegrationTest
    {

        [Fact]
        public async Task Test_GetDeductionCalc_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = Guid.Parse("DD177F4E-0D45-4F1A-9256-2EFAAB10D3E6");
                var response = await client.GetAsync("/api/DeductionCalc/" + employeeid);

                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<BenefitsDeductionResults>(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetDeductionCalc_Return404()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = Guid.NewGuid();
                var response = await client.GetAsync("/api/DeductionCalc/" + employeeid);

                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<BenefitsDeductionResults>(result);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
