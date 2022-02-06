using Microsoft.AspNetCore.Mvc;
using PE.ApiHelper.Entities;
using PE.EmployeeAPIService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PE.EmployeeAPIService.Common.Interface
{
    public interface IEmployeeRepository
    {
        //public IList RetrieveEmployeeData();
        public Task<IList> RetrieveEmployeeOnlyData();
        public Task<List<PaycheckTypes>> RetrieveAllPaycheckTypes();
        public Task<Employees> RetrieveEmployeeById(Guid employeeId);
        public Task UpdateEmployee(Guid id, UpdateEmployees updateEmployees);
        public Task<Employees> SaveEmployee(UpdateEmployees updateEmployees);
        public Task<Employees> DeleteEmployee(Guid employeeId);
    }
}
