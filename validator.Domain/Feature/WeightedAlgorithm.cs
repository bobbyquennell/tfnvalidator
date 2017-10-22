using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tfn.Domain.Feature
{
    public class WeightedAlgorithm : IValidateAlgorithm
    {
        public int getRemainder(int tfn)
        {
            //throw new NotImplementedException();
            var digits = tfn.ToString().Select( n =>n.ToString()).ToArray();
            //do the calcs
            var sum = (Convert.ToInt32(digits[0]) * 1)
                    + (Convert.ToInt32(digits[0]) * 4)
                    + (Convert.ToInt32(digits[0]) * 3)
                    + (Convert.ToInt32(digits[3]) * 7)
                    + (Convert.ToInt32(digits[4]) * 5)
                    + (Convert.ToInt32(digits[5]) * 8)
                    + (Convert.ToInt32(digits[6]) * 6)
                    + (Convert.ToInt32(digits[7]) * 9)
                    + (Convert.ToInt32(digits[8]) * 10);

            return sum % 11;
        }
    }
}
