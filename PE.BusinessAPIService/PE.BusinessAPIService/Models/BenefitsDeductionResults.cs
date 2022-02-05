using System.ComponentModel.DataAnnotations;

namespace PE.BusinessAPIService.Models
{    
    public class BenefitsDeductionResults
    {        
        public decimal EmployeeBenefitsDeductedPerPayCheck { get; set; }
        public decimal EmployeeBenefitsDeductedPerYear { get; set; }     
        public decimal DependentsBenefitsDeductedPerPayCheck { get; set; }
        public decimal DependentsBenefitsDeductedPerYear { get; set; }
        public decimal TotalBenefitsDeductedPerPayCheck { get; set; }
        public decimal TotalBenefitsDeductedPerPayYear { get; set; }
        public decimal TotalSalaryAfterDeducted { get; set; }

    }

}
