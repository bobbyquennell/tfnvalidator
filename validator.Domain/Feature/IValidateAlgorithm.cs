using System;
using System.Collections.Generic;
using System.Text;

namespace tfn.Domain.Feature
{
    public interface IValidateAlgorithm
    {
        int getRemainder(int tfn);
    }
}
