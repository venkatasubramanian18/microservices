using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using PE.BusinessAPIService.Common.CalcBenefitsDiscount;
using PE.BusinessAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static PE.BusinessAPIService.Common.Constants;

namespace PE.BusinessAPIService.Common.Calculator
{
    public class BenefitsDeductCalc : IBenefitsDeductCalc
    {
        private readonly PaylocityContext _context;
        private readonly INameBasedDiscount _nameBasedDiscount;
        private static decimal payCheckPerAnnum;
        internal Guid _employeeId { get; set; }
        internal static Employees employeesList { get; set; }
        internal static List<Persons> personsList { get; set; }

        public BenefitsDeductCalc(PaylocityContext context, INameBasedDiscount nameBasedDiscount)
        {
            _context = context;
            _nameBasedDiscount = nameBasedDiscount;
        }

        public void Initialize(Guid employeeId)
        {
            _employeeId = employeeId;
            employeesList = _context.Employees
                                .Where(x => x.EmployeeId == _employeeId)
                                .Include(x => x.Dependents)
                                .Include(x => x.Salaries).FirstOrDefault();

            //TODO: Need to change PaycheckType in database instead of converting here 
            payCheckPerAnnum = Convert.ToDecimal(
                                _context.PaycheckTypes
                                    .Where(pct => pct.PaycheckTypeId == employeesList.Salaries.First().PaycheckTypeId).First().PaycheckType
                                ); ;

            CreatePersonsList();
        }

        public static List<Persons> CreatePersonsList()
        {
            personsList = new List<Persons>();
            personsList
                .Add(
                new Persons()
                {
                    FirstName = employeesList.FirstName,
                    IsEmployee = Constants.IsEmployee
                });

            foreach (var dependent in employeesList.Dependents)
            {
                personsList
                    .Add(
                    new Persons()
                    {
                        FirstName = dependent.FirstName,
                        IsEmployee = !Constants.IsEmployee
                    });
            }

            return personsList;
        }

        public decimal BenefitsDeductPerPaycheckCalc(bool isEmployee)
        {
            return BenefitsDeductPerYearCalc(isEmployee) / payCheckPerAnnum;
        }

        public decimal BenefitsDeductPerYearCalc(bool isEmployee)
        {
            return 
                isEmployee 
                ? EmployeeBenefitsDeductPerYear(isEmployee) 
                : DependentsBenefitsDeductPerYear(isEmployee);
        }

        public decimal EmployeeBenefitsDeductPerYear(bool isEmployee) =>       
            Constants.EMPLOYEE_BENEFITS_PER_YEAR * (1 - _nameBasedDiscount.Discount(GetFirstName(isEmployee)));

        public decimal DependentsBenefitsDeductPerYear(bool isEmployee) =>
            personsList
                .Where(y => y.IsEmployee == isEmployee)
                .Sum(x => Constants.DEPENDENT_BENEFITS_PER_YEAR * (1 - _nameBasedDiscount.Discount(GetFirstName(isEmployee))));

        public string GetFirstName(bool isEmployee)
        {
            return personsList.First(x => x.IsEmployee == isEmployee).FirstName;
        }

        public decimal CalculatedDedecutedSalay()
        {
            return Convert.ToDecimal(employeesList.Salaries.First().Salary);
        }
    }
}
