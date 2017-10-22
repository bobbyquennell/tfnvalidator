using System;
using System.Collections.Generic;
using System.Text;

namespace validator.Domain.Feature
{
    public interface IValidateAlgorithm
    {
        int getRemainder(int tfn);
    }
}
