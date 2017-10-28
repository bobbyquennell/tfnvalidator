using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace validator.Domain.Feature
{
    public class TFNParser
    {
        public static bool IfTfnPairLinked(string tfn1, string tfn2, int min = 4)
        {
            string A = tfn2, B = tfn1;
            if(tfn1.Length >= tfn2.Length)
            {
                A = tfn1;
                B = tfn2;
            }
            for (int i = 0; i <= B.Length - min; i++) 
            {
                string matchingNum = B.Substring(i, min);
                if (A.Contains(matchingNum))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}
