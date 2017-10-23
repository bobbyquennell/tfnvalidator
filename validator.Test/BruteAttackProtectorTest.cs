using System;
using NUnit.Framework;
using validator.Domain.Feature;
using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace validator.Test
{
    [TestFixture]
    public class BruteAttackProtectorTest
    {
        [Test]
        public void Protector_Detected_Attack_3_Linked_TFN_Share_Same_Set()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543319876"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543419876"));//C
            //linked TFN: (A, B) (A, C) (B,C) shared set: "9876"

            Assert.That(result1, Is.EqualTo(false)); //input 1st TFN , (A,0)
            Assert.That(result2, Is.EqualTo(false)); //input 2nd TFN,  (A,B)
            Assert.That(result3, Is.EqualTo(true)); //input 3nd TFN, (B, C), (C, A)
        }

        [Test]
        public void Protector_Detected_Attack_3_Linked_TFN_Share_2_Sets()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543218629"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("862199876"));//C
            //1st linked TFN pair: (A, B) shared set: "54321" 
            //2nd linked TFN pair: (A, C) shared set: "9876"

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_No_Attack_Detected()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("37118629"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("37118655"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("37118660"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
        }

        [Test]
        public void Protector_No_Attack_Detected_OutOf_30Sec()
        {
            BruteAttackProtector.CreateTfnPool(1);//1 seconds
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543218629"));//B

            Thread.Sleep(1500);
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("862199876"));//C
            //1st linked TFN pair: (A, B) shared set: "54321" 
            //2nd linked TFN pair: (A, C) shared set: "9876"

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
        }
    }
}
