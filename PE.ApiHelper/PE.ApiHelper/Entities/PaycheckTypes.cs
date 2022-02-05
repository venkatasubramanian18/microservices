using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PE.ApiHelper.Entities
{
    public partial class PaycheckTypes
    {
        public PaycheckTypes()
        {
            Salaries = new HashSet<Salaries>();
        }

        public Guid PaycheckTypeId { get; set; }
        public string PaycheckType { get; set; }

        public virtual ICollection<Salaries> Salaries { get; set; }
    }
}
