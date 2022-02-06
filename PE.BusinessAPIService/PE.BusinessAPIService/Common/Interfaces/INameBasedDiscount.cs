namespace PE.BusinessAPIService.Common.CalcBenefitsDiscount
{
    public interface INameBasedDiscount
    {
        public decimal Discount(string name);
    }
}
