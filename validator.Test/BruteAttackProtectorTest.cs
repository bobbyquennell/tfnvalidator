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
        public void Protector_No_Attack_1()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("11112222"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("22223333"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("44445555"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
        }

        [Test]
        public void Protector_Detected_Attack_1()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("11112222"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("22223333"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("33334444"));//C

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }
        [Test]
        public void Protector_Detected_Attack_2()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("11112222"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("99999999"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("22223333"));
            bool result4 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("88888888"));
            bool result5 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("33334444"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
            Assert.That(result4, Is.EqualTo(false));
            Assert.That(result5, Is.EqualTo(true));
        }

        [Test]
        public void Protector_Detected_Attack_3()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("11112222"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("33334444"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("22223333"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_Detected_Attack_4()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("11112222"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("99111199"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("99991111"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_Detected_Attack_5()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("222223333"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("333334444"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_No_Attack_2()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("222211111"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("999999999"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
        }

        [Test]
        public void Protector_No_Attack_Detected_OutOf_30Sec()
        {
            BruteAttackProtector.CreateTfnPool(1);//1 seconds
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543329876"));//B

            Thread.Sleep(1500);
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543449876"));//C
            //1st linked TFN pair: (A, B) shared set: "54321" 
            //2nd linked TFN pair: (A, C) shared set: "9876"

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
        }
        [Test]
        public void Protector_Detected_Attack_6()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543329876"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543449876"));//C

            Assert.That(result1, Is.EqualTo(false)); //input 1st TFN , (A,0)
            Assert.That(result2, Is.EqualTo(false)); //input 2nd TFN,  (A,B)
            Assert.That(result3, Is.EqualTo(true)); //input 3nd TFN, (B, C), (C, A)
        }

        [Test]
        public void Protector_Detected_Attack_7()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("37118629"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("37118655"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("37118660"));//C

            Assert.That(result1, Is.EqualTo(false)); //input 1st TFN , (A,0)
            Assert.That(result2, Is.EqualTo(false)); //input 2nd TFN,  (A,B)
            Assert.That(result3, Is.EqualTo(true)); //input 3nd TFN, (B, C), (C, A)
        }

        [Test]
        public void Protector_Detected_Attack_8()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }
        [Test]
        public void Protector_Detected_Attack_9()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("222211111"));//B

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_Detected_Attack_10()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("222211111"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_Detected_Attack_11()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("111112222"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("222211111"));//B
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("222211111"));//B

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_Detected_Attack_12()
        {
            BruteAttackProtector.CreateTfnPool();
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("123456789"));
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("123459876"));
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));

            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(true));
        }

        [Test]
        public void Protector_No_Attack_12_OutOf_30Sec()
        {
            BruteAttackProtector.CreateTfnPool(1);//1 seconds
            bool result1 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("123456789"));//A
            bool result2 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("123459876"));//B

            Thread.Sleep(1500);
            bool result3 = BruteAttackProtector.IfBruteAttack(Convert.ToInt32("543219876"));//C


            Assert.That(result1, Is.EqualTo(false));
            Assert.That(result2, Is.EqualTo(false));
            Assert.That(result3, Is.EqualTo(false));
        }
    }
}
