using Microsoft.EntityFrameworkCore;
using PE.ApiHelper.Context;
using PE.ApiHelper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DependentAPIService.UnitTests
{
    internal class DependencyHelper: IDisposable
    {
        private static PaylocityContext context;

        public static Dependents dependents1 = new Dependents()
        {
            EmployeeId = Guid.NewGuid(),
            FirstName = "FirstNameTest1",
            LastName = "LastNameTest1",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.MinValue
        };

        public static Dependents dependents2 = new Dependents()
        {
            EmployeeId = Guid.NewGuid(),
            FirstName = "FirstNameTest2",
            LastName = "LastNameTest2",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.MinValue
        };

        public static DependentTypes dependentType1 = new DependentTypes()
        {
            DependentType = "Child"
        };

        public static DependentTypes dependentType2 = new DependentTypes()
        {
            DependentType = "Spouse"
        };

        public static List<DependentTypes> dependentTypesList = new List<DependentTypes>();

        public static List<Dependents> dependentlist = new List<Dependents>();

        public static PaylocityContext GetPaylocityContext()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<PaylocityContext>()
            .UseInMemoryDatabase(databaseName: "PaylocityDataBase")
            .Options;

            context = new PaylocityContext(options);

            if (DependentTypeExists("Child"))
                return context;

            //Create data for PaycheckTypes
            context.DependentTypes.Add(dependentType1);
            context.SaveChanges();

            //// Create mocked Context by seeding Data as per Schema///
            //Create entry for salaries in salaries table
            var dependentTypeId = context.DependentTypes.Where(x => x.DependentType == "Child").Select(y => y.DependentTypeId).FirstOrDefault();
            //Create data for employees
            if (DependentExists(dependents1.EmployeeId))
                return context;

            dependents1.DependentTypeId = dependentTypeId;
            dependents1.EmployeeId = Guid.NewGuid();

            context.Dependents.Add(dependents1);
            context.SaveChanges();

            dependents2.DependentTypeId = dependentTypeId;
            dependents2.EmployeeId = Guid.NewGuid();

            context.Dependents.Add(dependents2);
            context.SaveChanges();

            dependentlist = context.Dependents
                    .Include(x => x.DependentType)
                    .ToList();

            return context;
            
        }

        public void Dispose()
        {
        }

        private static bool DependentExists(Guid id)
        {
            return context.Dependents.Any(e => e.DependentId == id);
        }

        private static bool DependentTypeExists(string dependentType)
        {
            return context.DependentTypes.Any(e => e.DependentType == dependentType);
        }
    }
}
