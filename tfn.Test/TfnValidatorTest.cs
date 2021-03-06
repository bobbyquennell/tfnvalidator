﻿using System;
using NUnit.Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using tfn.Domain.Model;
using tfn.Domain.Feature;

namespace tfn.Test
{
    [TestFixture]
    public class TfnValidatorTest
    {
        [Test]
        public void Validate_TFN_9_Digits_Success()
        {
            TFNtoCheck model = GenerateNewTFNtoCheck(123456789);
            var alg = new WeightedAlgorithm();
            var validator = new TfnValidator(alg);

            var result = validator.Validate(model.Value);
            Assert.That(result, Is.EqualTo(0));
            //Assert.AreEqual(result, 0);
        }


        private TFNtoCheck GenerateNewTFNtoCheck(int v)
        {
            TFNtoCheck model = new TFNtoCheck
            {
                Value = v,
                TimeStamp = DateTime.Now,
                Linked = false,
                Remainder = 0
            };

            return model;
        }
    }
}
