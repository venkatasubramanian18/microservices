using System.Collections.Generic;
using System.Linq;

namespace PE.BusinessAPIService.Common.CalcBenefitsDiscount
{
    public class NameBasedDiscount : INameBasedDiscount
    {
        private Dictionary<string, decimal> FirstCharDiscountedNameList;

        public NameBasedDiscount() {
            FirstCharDiscountedNameList = new Dictionary<string, decimal>();
            FirstCharDiscountedNameList.Add("a", Constants.NAME_STARTS_WITH_A_DISCOUNT);
        }

        public decimal Discount(string name)
        {
            var nameBasedDis = FirstCharDiscountedNameList.SingleOrDefault(d => d.Key == name.Substring(0, 1).ToLower());
                return nameBasedDis.Value;
        }

    }
}
