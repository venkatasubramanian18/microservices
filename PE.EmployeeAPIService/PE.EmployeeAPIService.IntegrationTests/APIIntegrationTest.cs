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

namespace PE.EmployeeAPIService.IntegrationTests
{
    public class APIIntegrationTest
    {
        [Fact]
        public async Task Test_GetEmployeeData_All()
        {

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/employees");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetEmployeeById_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = Guid.Parse("BE3F05A6-2589-4B1C-A555-833ED4EF10C9");
                var response = await client.GetAsync("/api/employees/" + employeeid);

                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Employees>(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(actualResult.EmployeeId, employeeid);
            }
        }

        [Fact]
        public async Task Test_GetEmployeeById_Return404()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = new Guid();
                var response = await client.GetAsync("/api/employees/" + employeeid);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Employees>(result);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_UpdateEmployee_Return400()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = new Guid();
                var jsonString = "{\"employeeId\":\"B85F46EE-3683-EC11-9832-2C6DC102E08F\",\"firstName\":\"oi\",\"lastName\":\"yuyu\",\"createdDate\":\"0001-01-01T00:00:00\",\"modifiedDate\":\"0001-01-01T00:00:00\",\"dependents\":[],\"Salary\":99999,\"PaycheckType\":\"104\"}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/employees/" + employeeid, httpContent);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Employees>(result);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_UpdateEmployee_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeid = Guid.Parse("BE3F05A6-2589-4B1C-A555-833ED4EF10C9");
                var jsonString = "{\"employeeId\":\"BE3F05A6-2589-4B1C-A555-833ED4EF10C9\",\"firstName\":\"oi\",\"lastName\":\"yuyu\",\"createdDate\":\"0001-01-01T00:00:00\",\"modifiedDate\":\"0001-01-01T00:00:00\",\"dependents\":[],\"Salary\":99999,\"PaycheckType\":\"104\"}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/employees/" + employeeid, httpContent);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Employees>(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_SaveEmployee_Return200()
        {

            using (var client = new TestClientProvider().Client)
            {
                var jsonString = "{\"firstName\":\"integrationTest1\",\"lastName\":\"integrationTest2\",\"createdDate\":\"2022-02-06T09:34:06.054Z\",\"modifiedDate\":\"2022-02-06T09:34:06.054Z\",\"salary\":\"20000\",\"paycheckType\":\"26\"}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/employees", httpContent);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Employees>(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }
        }

        [Fact]
        public async Task Test_DeleteEmployee_Return400()
        {

            using (var client = new TestClientProvider().Client)
            {
                var employeeId = new Guid(); 
                var response = await client.DeleteAsync("/api/employees" + employeeId);
                var result = response.Content.ReadAsStringAsync().Result;

                var actualResult = JsonConvert.DeserializeObject<Employees>(result);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            }
        }
    }
}
