﻿using System;
using System.Collections.Generic;
using System.Text;
using validator.Domain.Model;

namespace validator.Domain.Feature
{
    public class TfnValidator
    {
        private IValidateAlgorithm _alg;
        //private WeightedAlgorithm alg;

        public TfnValidator(IValidateAlgorithm alg)
        {
            this._alg = alg;

        }

        //public TfnValidator(WeightedAlgorithm alg)
        //{
        //    this.alg = alg;
        //}

        public int Validate(int tfn)
        {
            int Remainder = _alg.getRemainder(tfn);
            return Remainder;
               
        }
    }
}
