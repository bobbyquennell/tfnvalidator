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
            int sum = 1;
            if(digits.Length == 9)
            {
                sum = (Convert.ToInt32(digits[0]) * 10)
                        + (Convert.ToInt32(digits[1]) * 7)
                        + (Convert.ToInt32(digits[2]) * 8)
                        + (Convert.ToInt32(digits[3]) * 4)
                        + (Convert.ToInt32(digits[4]) * 6)
                        + (Convert.ToInt32(digits[5]) * 3)
                        + (Convert.ToInt32(digits[6]) * 5)
                        + (Convert.ToInt32(digits[7]) * 2)
                        + (Convert.ToInt32(digits[8]) * 1);
            }
            else if (digits.Length == 8)
            {
                sum = (Convert.ToInt32(digits[0]) * 10)
                        + (Convert.ToInt32(digits[1]) * 7)
                        + (Convert.ToInt32(digits[2]) * 8)
                        + (Convert.ToInt32(digits[3]) * 4)
                        + (Convert.ToInt32(digits[4]) * 6)
                        + (Convert.ToInt32(digits[5]) * 3)
                        + (Convert.ToInt32(digits[6]) * 5)
                        + (Convert.ToInt32(digits[7]) * 1);
            }
            else {
                // TO DO: throw exception
            }
            

            return sum % 11;
        }
    }
}
