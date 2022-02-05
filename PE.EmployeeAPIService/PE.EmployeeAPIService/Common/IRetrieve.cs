using System.Collections;

namespace PE.EmployeeAPIService.Common
{
    public interface IRetrieve
    {
        public IList RetrieveEmployeeData();
        public IList RetrieveEmployeeOnlyData();
    }
}
