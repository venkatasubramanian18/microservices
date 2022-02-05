using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PE.ApiHelper.Entities
{
    public partial class DependentTypes
    {
        public DependentTypes()
        {
            Dependents = new HashSet<Dependents>();
        }

        public Guid DependentTypeId { get; set; }
        public string DependentType { get; set; }

        public virtual ICollection<Dependents> Dependents { get; set; }
    }
}
