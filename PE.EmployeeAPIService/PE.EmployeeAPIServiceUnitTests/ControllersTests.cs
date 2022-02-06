using Microsoft.AspNetCore.Mvc;
using Moq;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.EmployeeAPIService.Common;
using PE.EmployeeAPIService.Common.Interface;
using PE.EmployeeAPIService.Controllers;
using PE.EmployeeAPIService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PE.EmployeeAPIServiceUnitTests
{
    public class ControllersTests
    {

        private readonly Mock<IEmployeeRepository> _repository;

        public ControllersTests()
        {
            _repository = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async void GetEmployees_ReturnAll()
        {
            var context = DependencyHelper.GetPaylocityContext();
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            Employees employees = new Employees() {
                EmployeeId = employeeId
            };

            IList list = new List<Employees>() { employees };
            Mock<IEmployeeRepository> mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(m => m.RetrieveEmployeeOnlyData()).ReturnsAsync(list);

            var controller = new EmployeesController(mockRepo.Object);

            IList<Employees> listResult = (IList<Employees>)await controller.GetEmployees();

            Assert.True(listResult.Count > 0);
            Assert.Equal(employeeId, listResult.First().EmployeeId);

        }

        [Fact]
        public async void GetEmployeesById_Return200()
        {
            var employeeId = DependencyHelper.employee1.EmployeeId;

            Mock<IEmployeeRepository> mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(m => m.RetrieveEmployeeById(employeeId)).ReturnsAsync(DependencyHelper.employee1);

            var controller = new EmployeesController(mockRepo.Object);

            var actualResult = await controller.GetEmployees(employeeId).ConfigureAwait(false);
            var result = actualResult as OkObjectResult;
            var employee = (Employees)result.Value;

            Assert.NotNull(actualResult);
            Assert.True(employee.EmployeeId == employeeId);
            Assert.True(result.StatusCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async void GetEmployeesById_Return404()
        {
            var employees = new Employees();
            var employeeId = new Guid();

            Mock<IEmployeeRepository> mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(m => m.RetrieveEmployeeById(employeeId)).ReturnsAsync(employees);

            var controller = new EmployeesController(mockRepo.Object);

            var result = await controller.GetEmployees(employeeId).ConfigureAwait(false);
            var actualResult = result as ObjectResult;

            Assert.Null(actualResult);
            Assert.True(result.GetType().Name == "NotFoundResult");
        }

        [Fact]
        public async void GetAllPaycheckTypes_ReturnPaycheckTypes()
        {
            DependencyHelper.paycheckList.Add(DependencyHelper.paycheckType1);
            DependencyHelper.paycheckList.Add(DependencyHelper.paycheckType2);

            Mock<IEmployeeRepository> mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(m => m.RetrieveAllPaycheckTypes()).ReturnsAsync(DependencyHelper.paycheckList);

            var controller = new EmployeesController(mockRepo.Object);

            var result = await controller.GetPaycheckTypes().ConfigureAwait(false);
            var actualResult = result.Value;

            Assert.NotNull(actualResult);
            Assert.True(actualResult.Count == 2);

        }

        [Fact]
        public async void UpdateEmployee_Return200()
        {
            var context = DependencyHelper.GetPaylocityContext();
            var employeeId = context.Employees.First().EmployeeId;
            var updateEmployee = new UpdateEmployees()
            {
                EmployeeId = employeeId,
                FirstName = "Test123",
                LastName = context.Employees.First(x => x.EmployeeId == employeeId).LastName
            };

            Mock<IEmployeeRepository> mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(m => m.UpdateEmployee(employeeId, updateEmployee));

            var controller = new EmployeesController(mockRepo.Object);

            var result = await controller.PutEmployees(employeeId, updateEmployee).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.True(result.GetType().Name == "OkResult");

        }

        [Fact]
        public async void DeleteEmployee_Return404()
        {
            var context = DependencyHelper.GetPaylocityContext();
            var employeeId = context.Employees.First().EmployeeId;

            Mock<IEmployeeRepository> mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(m => m.DeleteEmployee(employeeId));

            var controller = new EmployeesController(mockRepo.Object);

            var result = await controller.DeleteEmployees(employeeId).ConfigureAwait(false);
            var actualResult = result.Result;

            Assert.Null(result.Value);
            Assert.True(actualResult.GetType().Name == "NotFoundResult");

        }

    }
}
