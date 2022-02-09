using System.Collections.Generic;
using System.Linq;

namespace PE.BusinessAPIService.Common.CalcBenefitsDiscount
{
    /// <summary>
    /// Name based Discount
    /// </summary>
    public class NameBasedDiscount : INameBasedDiscount
    {
        private Dictionary<string, decimal> FirstCharDiscountedNameList = new Dictionary<string, decimal>()
        {
            { "a", Constants.NAME_STARTS_WITH_A_DISCOUNT}
        };

        /// <summary>
        /// The method assigns the discounted value based on the 1st char of the name passed
        /// </summary>
        /// <param name="name"></param>
        /// <returns>disount in decimal</returns>
        public decimal Discount(string name)
        {           
            return FirstCharDiscountedNameList
                .SingleOrDefault(d => d.Key == name.Substring(0, 1).ToLower())
                .Value;
        }

    }
}
