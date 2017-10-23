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
        public void Parse_9_Digit_TFN()
        {
            IEnumerable<IEnumerable<int>> result = TFNParser.FindConsecutiveNumSets("123459876");
            IEnumerable<IEnumerable<int>> result2 = TFNParser.FindConsecutiveNumSets("543219876");
            IEnumerable<IEnumerable<int>> result3 = TFNParser.FindConsecutiveNumSets("123456789");

            Assert.That(result.Count, Is.EqualTo(4));

            Assert.That(result.ElementAt(0).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4 }));
            Assert.That(result.ElementAt(1).ToArray(), Is.EqualTo(new int[] { 2, 3, 4, 5 }));
            Assert.That(result.ElementAt(2).ToArray(), Is.EqualTo(new int[] { 9, 8, 7, 6 }));
            Assert.That(result.ElementAt(3).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4, 5 }));

            Assert.That(result2.Count, Is.EqualTo(4));

            Assert.That(result2.ElementAt(0).ToArray(), Is.EqualTo(new int[] { 5, 4, 3, 2 }));
            Assert.That(result2.ElementAt(1).ToArray(), Is.EqualTo(new int[] { 4, 3, 2, 1 }));
            Assert.That(result2.ElementAt(2).ToArray(), Is.EqualTo(new int[] { 9, 8, 7, 6 }));
            Assert.That(result2.ElementAt(3).ToArray(), Is.EqualTo(new int[] { 5, 4, 3, 2, 1 }));

            Assert.That(result3.Count, Is.EqualTo(21));

            Assert.That(result3.ElementAt(0).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4 }));
            Assert.That(result3.ElementAt(1).ToArray(), Is.EqualTo(new int[] { 2, 3, 4, 5 }));
            Assert.That(result3.ElementAt(2).ToArray(), Is.EqualTo(new int[] { 3, 4, 5, 6 }));
            Assert.That(result3.ElementAt(3).ToArray(), Is.EqualTo(new int[] { 4, 5, 6, 7 }));
            Assert.That(result3.ElementAt(4).ToArray(), Is.EqualTo(new int[] { 5, 6, 7, 8 }));
            Assert.That(result3.ElementAt(5).ToArray(), Is.EqualTo(new int[] { 6, 7, 8, 9 }));
                              
            Assert.That(result3.ElementAt(6).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4, 5 }));
            Assert.That(result3.ElementAt(7).ToArray(), Is.EqualTo(new int[] { 2, 3, 4, 5, 6 }));
            Assert.That(result3.ElementAt(8).ToArray(), Is.EqualTo(new int[] { 3, 4, 5, 6, 7 }));
            Assert.That(result3.ElementAt(9).ToArray(), Is.EqualTo(new int[] { 4, 5, 6, 7, 8 }));
            Assert.That(result3.ElementAt(10).ToArray(), Is.EqualTo(new int[] { 5, 6, 7, 8, 9 }));
                              
            Assert.That(result3.ElementAt(11).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4, 5, 6 }));
            Assert.That(result3.ElementAt(12).ToArray(), Is.EqualTo(new int[] { 2, 3, 4, 5, 6, 7 }));
            Assert.That(result3.ElementAt(13).ToArray(), Is.EqualTo(new int[] { 3, 4, 5, 6, 7, 8 }));
            Assert.That(result3.ElementAt(14).ToArray(), Is.EqualTo(new int[] { 4, 5, 6, 7, 8, 9 }));
                              
            Assert.That(result3.ElementAt(15).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4, 5, 6, 7 }));
            Assert.That(result3.ElementAt(16).ToArray(), Is.EqualTo(new int[] { 2, 3, 4, 5, 6, 7, 8 }));
            Assert.That(result3.ElementAt(17).ToArray(), Is.EqualTo(new int[] { 3, 4, 5, 6, 7, 8, 9 }));
                              
            Assert.That(result3.ElementAt(18).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
            Assert.That(result3.ElementAt(19).ToArray(), Is.EqualTo(new int[] { 2, 3, 4, 5, 6, 7, 8, 9 }));
                              
            Assert.That(result3.ElementAt(20).ToArray(), Is.EqualTo(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));


        }

    }
}


