using System;

namespace PE.EmployeeAPIService.Models
{
    public class EmployeeData
    {
        public Guid EmployeeId { get; set; }
        public string EmployeesFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public Guid DependentTypeId { get; set; }
        public string DependentFirstName { get; set; }
        public string DependentFLastName { get; set; }
        public string DependentType { get; set; }
        public Guid SalaryId { get; set; }
        public string EmployeeSalary { get; set; }
        public string EmployeePaycheckType { get; set; }

    }
}
