using Microsoft.AspNetCore.Mvc;
using PE.ApiHelper.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PE.DependentAPIService.Common.Interfaces
{
    public interface IDependentRepository
    {
        Task<IList<Dependents>> RetrieveDependentsData();
        Task<int> RetrieveDependentCountById(Guid id);
        Task<IList<DependentTypes>> RetrieveDependentTypes();
        Task<Dependents> SaveDependents(Dependents dependents);
        Task UpdatetDependent(Guid id, Dependents dependents);
        Task<Dependents> DeleteDependent(Guid id);
        bool DependentExists(Guid id);
    }
}
