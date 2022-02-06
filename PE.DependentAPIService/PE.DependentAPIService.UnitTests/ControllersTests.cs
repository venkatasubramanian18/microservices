using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PE.ApiHelper.Entities;
using PE.DependentAPIService.Common.Interfaces;
using PE.DependentAPIService.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PE.DependentAPIService.UnitTests
{
    public class ControllersTests
    {

        private readonly Mock<IDependentRepository> _repository;

        public ControllersTests()
        {
            _repository = new Mock<IDependentRepository>();
        }

        [Fact]
        public async void GetDependents_ReturnAll()
        {
            var context = DependencyHelper.GetPaylocityContext();

            Mock<IDependentRepository> mockRepo = new Mock<IDependentRepository>();
            mockRepo.Setup(m => m.RetrieveDependentsData()).ReturnsAsync(DependencyHelper.dependentlist);

            var controller = new DependentsController(mockRepo.Object);

            var actualResult = await controller.GetDependents().ConfigureAwait(false);
            var result = actualResult.Result as OkObjectResult;

            IList<Dependents> dependent = (IList<Dependents>)result.Value;

            Assert.True(dependent.Count > 0);
            Assert.True(dependent.Any());

        }

        [Fact]
        public async void GetDependentCountById_Return200()
        {
            var employeeId = DependencyHelper.GetPaylocityContext().Dependents.FirstOrDefault().EmployeeId;

            Mock<IDependentRepository> mockRepo = new Mock<IDependentRepository>();
            mockRepo.Setup(m => m.RetrieveDependentCountById(employeeId)).ReturnsAsync(2);

            var controller = new DependentsController(mockRepo.Object);

            var actualResult = await controller.GetDependents(employeeId).ConfigureAwait(false);
            var result = actualResult as OkObjectResult;

            Assert.NotNull(result);
            Assert.True(result.StatusCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async void GetDependentCountById_Return404()
        {
            var employeeId = new Guid();
            Mock<IDependentRepository> mockRepo = new Mock<IDependentRepository>();
            mockRepo.Setup(m => m.RetrieveDependentCountById(employeeId)).ReturnsAsync(0);

            var controller = new DependentsController(mockRepo.Object);

            var actualResult = await controller.GetDependents(employeeId).ConfigureAwait(false);
            var result = actualResult as StatusCodeResult;

            Assert.NotNull(result);
            Assert.True(result.StatusCode == (int)HttpStatusCode.NotFound);
        }
    }
}
