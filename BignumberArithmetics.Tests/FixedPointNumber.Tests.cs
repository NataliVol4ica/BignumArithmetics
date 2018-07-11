﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace net.NataliVol4ica.BignumArithmetics.Tests
{
    //TODO: RENAME METHODS
    [TestClass]
    public class FixedPointNumberTests
    {
        [TestMethod]
        [ExpectedException(typeof(NumberFormatException),
        "Cannot create FixedPointNumber of \"null\"")]
        public void NullString_In_Constructor()
        {
            FixedPointNumber empty = new FixedPointNumber(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NumberFormatException),
        "Cannot create FixedPointNumber of \" +.5\"")]
        public void NumberWithoutInteger_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber(" +.5");
        }

        [TestMethod]
        [ExpectedException(typeof(NumberFormatException),
        "Cannot create FixedPointNumber of \"1234.\"")]
        public void NumberWithDotWithoutFrac_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber("1234.");
        }

        [TestMethod]
        [ExpectedException(typeof(NumberFormatException),
        "Cannot create FixedPointNumber of \"1.14.5\"")]
        public void NumWithTwoDots_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber("1.14.5");
        }

        [TestMethod]
        [ExpectedException(typeof(NumberFormatException),
        "Cannot create FixedPointNumber of \"  1a12.3 \"")]
        public void NumberWithAlpha_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber("  1a12.3 ");
        }

        [TestMethod]
        public void Zero_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber("   0     ");

            Assert.AreEqual("0", actual.ToString());
        }

        [TestMethod]
        public void PlusZero_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber(" +0");

            Assert.AreEqual("0", actual.ToString());
        }

        [TestMethod]
        public void MinusZero_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber(" -0");

            Assert.AreEqual("0", actual.ToString());
        }

        [TestMethod]
        public void Plus12Dot3_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber(" +12.3");

            Assert.AreEqual("12.3", actual.ToString());
        }

        [TestMethod]
        public void Minus17_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber(" -17    ");

            Assert.AreEqual("-17", actual.ToString());
        }

        [TestMethod]
        public void RandomBigNumber_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber("    -6473794237942.4723984729");

            Assert.AreEqual("-6473794237942.4723984729", actual.ToString());
        }

        [TestMethod]
        public void Number_12Dot0_In_Constructor()
        {
            FixedPointNumber actual = new FixedPointNumber("12.0");

            Assert.AreEqual("12.0", actual.ToString());
        }

    }
}
