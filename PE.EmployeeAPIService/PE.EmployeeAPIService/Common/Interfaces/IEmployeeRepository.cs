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
        Task<IList> RetrieveEmployeeOnlyData();
        Task<List<PaycheckTypes>> RetrieveAllPaycheckTypes();
        Task<Employees> RetrieveEmployeeById(Guid employeeId);
        Task UpdateEmployee(Guid id, UpdateEmployees updateEmployees);
        Task<Employees> SaveEmployee(UpdateEmployees updateEmployees);
        Task<Employees> DeleteEmployee(Guid employeeId);
    }
}
