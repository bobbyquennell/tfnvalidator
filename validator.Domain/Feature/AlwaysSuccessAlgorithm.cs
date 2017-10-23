using System;
using System.Collections.Generic;
using System.Text;

namespace validator.Domain.Feature
{
    public class AlwaysSuccessAlgorithm : IValidateAlgorithm
    {
        public int getRemainder(int tfn)
        {
            return 0;
        }
    }
}
