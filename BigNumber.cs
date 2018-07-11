﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//todo: force children to overload operators

namespace net.NataliVol4ica.BignumArithmetics
{
    /// <summary>
    /// Abstract class for big numbers
    /// </summary>
    public abstract class BigNumber
    {
        /* === Variables === */
        protected string _cleanString = null;
        protected int _sign = 1;
        
        /* === Properties === */
        /// <summary>The CleanString property represents number without spaces, extra zeroes etc</summary>
        /// <value> The CleanString property gets/sets the value of the string field, _cleanString</value>
        public string CleanString
        {
            get
            {
                return _cleanString;
            }
            protected set
            {
                _cleanString = value;
            }
        }
        /// <summary>The Sign property represents sign of the number</summary>
        /// <value> The CleanString property gets/sets the value of the int field, _sign</value>
        public int Sign
        {
            get
            {
                return _sign;
            }
            protected set
            {
                _sign = value;
            }
        }

        /*
        protected List<int> Digits = new List<int>();
        public int Size
        {
            get
            {
                return Digits.Count;
            }
        }*/

        /// <summary>The CleanStringRegEx property represents RegEx that is used in input parsing</summary>
        /// <value> The CleanString property gets the RegEx string</value>
        protected abstract string CleanStringRegEx
        {
            get;
        }

        /* === Abstarct Methods === */
        public abstract BigNumber Sum(BigNumber op);
        public abstract BigNumber Dif(BigNumber op);
        public abstract BigNumber Mul(BigNumber op);
        public abstract BigNumber Div(BigNumber op);

        /* === Static Methods === */
        /// <summary>
        /// Converts numbers into matching char symbols
        /// </summary>
        /// <param name="digit">Number to be converted. Must be in [0..15] range</param>
        /// <returns>A matching char; '0' if digit does not match limits</returns>
        public static char ToChar(int digit)
        {
            if (digit >= 0 && digit < 10)
                return digit.ToString()[0];
            if (digit > 9 && digit < 16)
                return Convert.ToChar('a' + digit - 10);
            return '0';
        }
        /// <summary>
        /// Converts [0..9a..dA..D] characters to matching number
        /// </summary>
        /// <param name="c">Character to be converted</param>
        /// <returns>A matching number; -1 if c does not match limits.</returns>
        public static int ToDigit(char c)
        {
            if (Char.IsDigit(c))
                return Convert.ToInt32(c - '0');
            if (char.IsLetter(c))
            {
                c = Char.ToLower(c);
                if (c < 'g')
                    return c - 'a' + 10;
            }
            return -1;
        }
        /// <summary>
        /// Swaps two objects of type T
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="A">First parameter to swap</param>
        /// <param name="B">Second parameter to swap</param>
        public static void Swap<T>(ref T A, ref T B)
        {
            T buf = A;
            A = B;
            B = buf;
        }
       
        /* === Protected Methods === */
        protected string CleanNumericString(string RawString, ref int sign)
        {
            string substr;

            substr = Regex.Match(RawString, CleanStringRegEx).Value;
            if (substr == "")
            {
                sign = 1;
                return "0";
            }
             sign = RawString.Contains("-") ? -1 : 1;
             return substr;
        }

        /* === Operators === */
        public static BigNumber operator +(BigNumber A, BigNumber B)
        {
            if (A is null || B is null)
                return null;
            return A.Sum(B);
        }
        public static BigNumber operator -(BigNumber A, BigNumber B)
        {
            if (A is null || B is null)
                return null;
            return A.Dif(B);
        }
        public static BigNumber operator *(BigNumber A, BigNumber B)
        {
            if (A is null || B is null)
                return null;
            return A.Mul(B);
        }
        public static BigNumber operator /(BigNumber A, BigNumber B)
        {
            if (A is null || B is null)
                return null;
            return A.Div(B);
        }
    }
}
