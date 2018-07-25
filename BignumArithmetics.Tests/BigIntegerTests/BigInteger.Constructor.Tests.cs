﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BignumArithmetics.BigIntegerTests
{
    [TestClass]
    public class BigIntegerConstructorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullString_In_Constructor()
        {
            BigInteger empty = new BigInteger((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberWithoutInteger_In_Constructor()
        {
            BigInteger actual = new BigInteger(" +.5");

            Assert.AreEqual("0", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberWithDotWithoutFrac_In_Constructor()
        {
            BigInteger actual = new BigInteger("1234.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumWithTwoDots_In_Constructor()
        {
            BigInteger actual = new BigInteger("1.14.5");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberWithAlpha_In_Constructor()
        {
            BigInteger actual = new BigInteger("  1a12.3 ");
        }

        [TestMethod]
        public void Zero_In_Constructor()
        {
            BigInteger actual = new BigInteger("   0     ");

            Assert.AreEqual("0", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }

        [TestMethod]
        public void PlusZero_In_Constructor()
        {
            BigInteger actual = new BigInteger(" +0");

            Assert.AreEqual("0", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }
        
        [TestMethod]
        public void MinusZero_In_Constructor()
        {
            BigInteger actual = new BigInteger(" -0");

            Assert.AreEqual("0", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }

        [TestMethod]
        public void Plus123_In_Constructor()
        {
            BigInteger actual = new BigInteger(" +123");

            Assert.AreEqual("123", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }

        [TestMethod]
        public void Minus17_In_Constructor()
        {
            BigInteger actual = new BigInteger(" -17    ");

            Assert.AreEqual("-17", actual.ToString());
            Assert.AreEqual(-1, actual.Sign);
        }

        [TestMethod]
        public void RandomBigNumber_In_Constructor()
        {
            BigInteger actual = new BigInteger("    -64737942379424723984729");

            Assert.AreEqual("-64737942379424723984729", actual.ToString());
            Assert.AreEqual(-1, actual.Sign);
        }

        [TestMethod]
        public void ManyZeros_ToString()
        {
            BigInteger actual = new BigInteger("-0000");

            Assert.AreEqual("0", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ManyZerosDotManyZeros_ToString()
        {
            BigInteger actual = new BigInteger("0000.000");
        }

        [TestMethod]
        public void PlusHeadingZeros_ToString()
        {
            BigInteger actual = new BigInteger("+00001234");

            Assert.AreEqual("1234", actual.ToString());
            Assert.AreEqual(1, actual.Sign);
        }

        [TestMethod]
        public void MinusHeadingZeros_ToString()
        { 
            BigInteger actual = new BigInteger("-000012340024823000");

            Assert.AreEqual("-12340024823000", actual.ToString());
            Assert.AreEqual(-1, actual.Sign);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Minus0Dot5_ToString()
        {
            BigInteger actual = new BigInteger("-0.5");
        }
    }
}
