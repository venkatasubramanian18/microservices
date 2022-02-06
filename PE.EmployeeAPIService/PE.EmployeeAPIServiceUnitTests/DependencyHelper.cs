using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.EmployeeAPIServiceUnitTests
{
    internal class DependencyHelper: IDisposable
    {
        private static PaylocityContext context;

        public static Employees employee1 = new Employees()
        {
            EmployeeId = Guid.NewGuid(),
            FirstName = "FirstNameTest1",
            LastName = "LastNameTest1",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.MinValue
        };

        public static Employees employee2 = new Employees()
        {
            EmployeeId = Guid.NewGuid(),
            FirstName = "FirstNameTest2",
            LastName = "LastNameTest2",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.MinValue
        };

        public static PaycheckTypes paycheckType1 = new PaycheckTypes()
        {
            PaycheckType = "26"
        };

        public static PaycheckTypes paycheckType2 = new PaycheckTypes()
        {
            PaycheckType = "52"
        };

        public static List<PaycheckTypes> paycheckList = new List<PaycheckTypes>();

        public static PaylocityContext GetPaylocityContext()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<PaylocityContext>()
            .UseInMemoryDatabase(databaseName: "PaylocityDataBase")
            .Options;

            context = new PaylocityContext(options);

            //// Create mocked Context by seeding Data as per Schema///
            //Create data for employees

            if (EmployeeExists(employee1.EmployeeId))
                return context;

            context.Employees.Add(employee1);
            context.SaveChanges();

            //Create data for PaycheckTypes
            context.PaycheckTypes.Add(paycheckType1);
            context.SaveChanges();

            //Create entry for salaries in salaries table
            var paycheckID = context.PaycheckTypes.Where(x => x.PaycheckType == "26").Select(y => y.PaycheckTypeId).FirstOrDefault();
            Salaries salaries = new Salaries() { EmployeeId = employee1.EmployeeId, Salary = "123456", SalaryId = new Guid(), PaycheckTypeId = paycheckID };
            context.Salaries.Add(salaries);

            context.SaveChanges();

            //Create data for employees
            context.Employees.Add(employee2);
            context.SaveChanges();

            //Create data for PaycheckTypes
            context.PaycheckTypes.Add(paycheckType2);
            context.SaveChanges();

            //Create entry for salaries in salaries table
            paycheckID = context.PaycheckTypes.Where(x => x.PaycheckType == "52").Select(y => y.PaycheckTypeId).FirstOrDefault();
            salaries = new Salaries() { EmployeeId = employee2.EmployeeId, Salary = "789101", SalaryId = new Guid(), PaycheckTypeId = paycheckID };
            context.Salaries.Add(salaries);

            context.SaveChanges();

            return context;
            
        }

        public void Dispose()
        {
        }

        private static bool EmployeeExists(Guid id)
        {
            return context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
