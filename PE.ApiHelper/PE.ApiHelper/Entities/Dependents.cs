using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PE.ApiHelper.Entities
{
    public partial class Dependents
    {
        public Guid DependentId { get; set; }
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid DependentTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual DependentTypes DependentType { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
