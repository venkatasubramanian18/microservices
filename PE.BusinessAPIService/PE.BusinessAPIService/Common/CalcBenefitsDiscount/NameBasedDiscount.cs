namespace PE.BusinessAPIService.Common.CalcBenefitsDiscount
{
    public class NameBasedDiscount : INameBasedDiscount
    {
        public decimal Discount(string name)
        {
            if (name.ToLower().StartsWith("a"))
                return Constants.NAME_STARTS_WITH_A_DISCOUNT;

            return decimal.Zero;
        }

    }
}
