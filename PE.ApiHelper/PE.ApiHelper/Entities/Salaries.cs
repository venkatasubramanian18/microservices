using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PE.ApiHelper.Entities
{
    public partial class Salaries
    {
        public Guid SalaryId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid PaycheckTypeId { get; set; }
        public string Salary { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        public virtual Employees Employee { get; set; }
        [JsonIgnore]
        public virtual PaycheckTypes PaycheckType { get; set; }
    }
}
