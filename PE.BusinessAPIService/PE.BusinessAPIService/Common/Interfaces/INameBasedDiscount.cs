namespace PE.BusinessAPIService.Common.CalcBenefitsDiscount
{
    public interface INameBasedDiscount
    {
        decimal Discount(string name);
    }
}
