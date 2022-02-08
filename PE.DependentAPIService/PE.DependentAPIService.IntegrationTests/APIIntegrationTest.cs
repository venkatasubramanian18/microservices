using Newtonsoft.Json;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PE.DependentAPIService.IntegrationTests
{
    public class APIIntegrationTest
    {
        [Fact]
        public async Task Test_GetDependentData_All()
        {

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/dependents");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetDependentCountById_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = Guid.Parse("DD177F4E-0D45-4F1A-9256-2EFAAB10D3E6");
                var response = await client.GetAsync("/api/dependents/" + employeeid);

                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetDependentCountById_Return404()
        {
            using (var client = new TestClientProvider().Client)
            {
                var employeeid = new Guid();
                var response = await client.GetAsync("/api/dependents/" + employeeid);

                var result = response.Content.ReadAsStringAsync().Result;

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_UpdateDependent_Return400()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = new Guid();
                var jsonString = "{\"dependentId\":\"fd3a104a-7382-ec11-9831-2c6dc102e08c\",\"employeeId\":\"fd3a104a-7382-ec11-9831-2c6dc102e08f\",\"firstName\":\"Manju\",\"lastName\":\"preethi\",\"dependentTypeId\":\"763B3285-B81B-4C0C-818C-E2B326205A3E\",\"createdDate\":\"0001-01-01T00:00:00\",\"modifiedDate\":\"0001-01-01T00:00:00\",\"dependentType\":null,\"employee\":null}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/dependents/" + employeeid, httpContent);
                var result = response.Content.ReadAsStringAsync().Result;

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_UpdateDependent_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var dependentid = Guid.Parse("ABB3AEB5-F287-EC11-983A-2C6DC102E08F");
                var jsonString = "{\"dependentId\":\"ABB3AEB5-F287-EC11-983A-2C6DC102E08F\",\"employeeId\":\"DD177F4E-0D45-4F1A-9256-2EFAAB10D3E6\",\"firstName\":\"Manju\",\"lastName\":\"Preethi\",\"dependentTypeId\":\"A0A14B48-62A6-4998-924C-4D6D1168254D\",\"createdDate\":\"0001-01-01T00:00:00\",\"modifiedDate\":\"0001-01-01T00:00:00\"}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/dependents/" + dependentid, httpContent);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Dependents>(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_SaveDependent_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var jsonString = "{\"dependentId\":\"00000000-0000-0000-0000-000000000000\",\"employeeId\":\"DD177F4E-0D45-4F1A-9256-2EFAAB10D3E6\",\"firstName\":\"Manju\",\"lastName\":\"preethi\",\"dependentTypeId\":\"47A61BEA-2F05-4C2D-BFF9-CCD1ECB3D3BC\",\"createdDate\":\"0001-01-01T00:00:00\",\"modifiedDate\":\"0001-01-01T00:00:00\",\"dependentType\":null,\"employee\":null}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/dependents/", httpContent);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Dependents>(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_DeleteDependent_Return400()
        {

            using (var client = new TestClientProvider().Client)
            {
                var dependentId = new Guid(); 
                var response = await client.DeleteAsync("/api/dependents" + dependentId);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Dependents>(result);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            }
        }
    }
}
