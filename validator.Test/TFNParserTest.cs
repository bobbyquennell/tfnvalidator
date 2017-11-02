using System;
using NUnit.Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using validator.Domain.Model;
using validator.Domain.Feature;
using System.Linq;
using System.Collections.Generic;

namespace tfn.Test
{
    [TestFixture]
    public class TFNParserTest
    {
        [Test]
        public void ShouldBeAbleToDetectLinkedTfnPair()
        {
            bool result1 = TFNParser.IfTfnPairLinked("123459876", "543219876");//on "9876"
            bool result2 = TFNParser.IfTfnPairLinked("543219876", "543219876");//on 9876
            bool result3 = TFNParser.IfTfnPairLinked("37118629", "37118655");// on 3711


            Assert.That(result1, Is.EqualTo(true));
            Assert.That(result1, Is.EqualTo(true));
            Assert.That(result1, Is.EqualTo(true));
        }

        [Test]
        public void ShouldBeAbleToDetectNoLinkedTfnPair()
        {
            bool result1 = TFNParser.IfTfnPairLinked("123459876", "543217876");
            bool result2 = TFNParser.IfTfnPairLinked("543219876", "543319776");
            bool result3 = TFNParser.IfTfnPairLinked("37118629", "37218655");


            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result1, Is.EqualTo(false));
        }

    }
}


