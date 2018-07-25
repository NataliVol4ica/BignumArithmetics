﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace BignumArithmetics
{
    /// <summary>Сlass for integer 
    /// big numbers</summary>
    public class BigInteger : BigNumber
    {
        #region Constructors
        /// <summary>Constructor creates a BigInteger equal to 0</summary>
        /// <returns>An instance of BigInteger</returns>
        public BigInteger() { }
        /// <summary>Constructor creates a BigInteger equal to parameter</summary>
        /// <param name="from">any BigInteger</param>
        /// <returns>An instance of BigInteger equal to parameter</returns>
        public BigInteger(BigInteger from)
        {
            CleanString = from.CleanString;
            if (from.Sign < 0)
                Negate();
        }
        /// <summary>Private constructor creates a BigInteger from a valid string 
        /// that is matching <see cref="validStringRegEx"/> 
        /// and is cut with <see cref="cleanStringRegEx"/></summary>
        /// <param name="str">string representing number digits and delimiter</param>
        /// <param name="sign">integer representing number sign</param>
        /// <returns>An instance of BigInteger</returns>
        private BigInteger(string str, int sign)
        {
            CleanString = str;
            if (sign < 0)
                Negate();
        }
        #endregion

        #region Static Methods
        /// <summary>Converts numbers into matching char symbols</summary>
        /// <param name="digit">Number to be converted. Must be in [0..9] range</param>
        /// <returns>A matching char; '0' if digit does not match limits</returns>
        public static char ToChar(int digit)
        {
            if (digit >= 0 && digit < 10)
                return digit.ToString()[0];
            return '0';
        }
        /// <summary>Converts characters to matching number</summary>
        /// <param name="c">Character to be converted. Must consist of ['0'..'9']</param>
        /// <returns>A matching number; -1 if parameter does not match limits.</returns>
        public static int ToDigit(char c)
        {
            if (Char.IsDigit(c))
                return Convert.ToInt32(c - '0');
            return -1;
        }

        /// <summary>Fabric thar returns an instance of BigInteger constructed from a string
        /// that is matching <see cref="validStringRegEx"/> 
        /// and is cut with <see cref="cleanStringRegEx"/></summary>
        /// <param name="str">String that represents a number</param>
        /// <returns>An instance of BigInteger. null if parameter is invalid</returns>
        public static BigInteger CreateFromString(string str)
        {
            if (string.IsNullOrEmpty(str) ||
                string.IsNullOrEmpty(validStringRegEx.Match(str).Value))
                return new BigInteger();
            str = CleanNumericString(str, out int sign);
            return new BigInteger(str, sign);
        }
        /// <summary>Fabric thar returns an instance of BigInteger constructed from a number</summary>
        /// <param name="number">Object of any numeric type</param>
        /// <returns>An instance of BigInteger. null if parameter is invalid</returns>
        public static BigInteger CreateFromNumber<T>(T number)
        {
            string str = number.ToString();
            return CreateFromString(str);
        }
        /// <summary>Сonverts BigInteger into reversed digit list with padding zeroes</summary>
        /// <param name="desiredInt">int represening how many digits should integer part contain</param>
        /// <param name="desiredFrac">int represening how many digits should fractional part contain</param>
        /// <returns>List of digits</returns>
        public static List<int> BigIntegerToIntList(BigInteger num, int desiredInt = 0)
        {
            if (num is null)
                return null;

            List<int> ret = new List<int>();
            int IntZeros;

            IntZeros = Math.Max(num.CleanString.Length, desiredInt) - num.CleanString.Length;
            for (int i = num.CleanString.Length - 1; i >= 0; i--)
                ret.Add(ToDigit(num.CleanString[i]));
            ret.AddRange(Enumerable.Repeat(0, IntZeros));
            return ret;
        }

        /// <summary>IntListToString method converts digit list to string</summary>
        /// <param name="digits">List of digits</param>
        /// <param name="dotPos">Integer representing reversed position of dot in string</param>
        /// <returns>A string representing a number; null in case of invalid arguments</returns>
        public static string IntListToString(List<int> digits)
        {
            int i;
            StringBuilder sb;

            if (digits is null || digits.Count == 0)
                return "";
            sb = new StringBuilder();           
            for (i = digits.Count - 1; i >= 0; i--)
                sb.Append(ToChar(digits[i]));           
            return sb.ToString();
        }
        /// <summary>CleanNumericString method cleans digit string with <see cref="cleanStringRegEx"/></summary>
        /// <param name="RawString">String representing of digits</param>
        /// <param name="sign">Integer representing position of dot in cleaned string</param>
        /// <returns>A clean string; "0" in case of invalid arguments</returns>
        protected static string CleanNumericString(string RawString, out int sign)
        {
            string substr;

            if (RawString is null)
            {
                sign = 1;
                return "0";
            }
            substr = cleanStringRegEx.Match(RawString).Value;
            if (substr == "")
            {
                sign = 1;
                return "0";
            }
            sign = RawString.Contains("-") ? -1 : 1;
            return substr;
        }
        #endregion

        #region Public tools
        /// <summary>Returns a copy of BigInteger instance </summary>
        /// <returns>Copy of BigInteger instance</returns>
        public BigInteger Copy()
        {
            return new BigInteger(this);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return (this == obj as BigInteger);
        }
        /// <summary> Returns |BigInteger| </summary>
        /// <param name="bf">BigInteger parameter</param>
        /// <returns>|BigInteger|</returns>
        public static BigInteger Abs(BigInteger bf)
        {
            BigInteger ret = new BigInteger(bf);
            if (bf.Sign < 0)
                bf.Negate();
            return bf;
        }
        #endregion

        #region Parent Overrides
        /// <summary>Normalizes reversed list of digits</summary>
        /// <param name="digits">List of digits</param>
        public override void NormalizeList(List<int> digits)
        {
            int i;

            if (digits is null || digits.Count == 0)
                return;
            for (i = 0; i < digits.Count - 1; i++)
            {
                if (digits[i] < 0)
                {
                    digits[i] += 10;
                    digits[i + 1]--;
                }
                else
                {
                    digits[i + 1] += digits[i] / 10;
                    digits[i] %= 10;
                }
            }
            while (digits[i] > 9)
            {
                digits.Add(digits[i] / 10);
                digits[i] %= 10;
                i++;
            }
        }

        /// <summary>Method that is calculating (this + op) for BigInteger objects.
        /// Overrided from parent</summary>
        /// <param name="op">Second operand</param>
        /// <returns>BigInteger equal to (this + op)  upcasted to BigNumber</returns>
        public override BigNumber Add(BigNumber op)
        {
            if (!(op is BigInteger))
                throw new ArgumentException("Cannot Add BigInteger and " + op.GetType());

            BigInteger bfLeft = this;
            BigInteger bfRight = (BigInteger)op;

            if (bfLeft.Sign != bfRight.Sign)
                return bfLeft.Substract(-bfRight);

            int desiredInt = Math.Max(bfLeft.CleanString.Length, bfRight.CleanString.Length);
            var leftList = BigIntegerToIntList(bfLeft, desiredInt);
            var rightList = BigIntegerToIntList(bfRight, desiredInt);
            var resultList = leftList.SumWithList(rightList);
            NormalizeList(resultList);

            BigInteger bfAns = CreateFromString(IntListToString(resultList));
            if (Sign < 0)
                bfAns.Negate();
            return bfAns;
        }
        /// <summary>Method that is calculating (this - op) for BigInteger objects.
        /// Overrided from parent</summary>
        /// <param name="op">Second operand</param>
        /// <returns>BigInteger equal to (this - op)  upcasted to BigNumber</returns>
        public override BigNumber Substract(BigNumber op)
        {
            if (!(op is BigInteger))
                throw new ArgumentException("Cannot Add BigInteger and " + op.GetType());

            BigInteger bfLeft = this;
            BigInteger bfRight = (BigInteger)op;

            if (bfLeft.Sign > 0 && bfRight.Sign < 0)
                return bfLeft.Add(-bfRight);
            if (bfLeft.Sign < 0 && bfRight.Sign > 0)
                return -(BigInteger)bfRight.Add(-bfLeft);
            if (bfLeft.Sign < 0 && bfRight.Sign < 0)
                return (-bfRight).Substract(-bfLeft);
            //both operands are > 0 here
            int sign = 1;

            if (bfLeft < bfRight)
            {
                sign = -sign;
                Swap(ref bfLeft, ref bfRight);
            }
            int desiredInt = Math.Max(bfLeft.CleanString.Length, bfRight.CleanString.Length);
            var leftList = BigIntegerToIntList(bfLeft, desiredInt);
            var rightList = BigIntegerToIntList(bfRight, desiredInt);
            var resultList = leftList.SubByList(rightList);
            NormalizeList(resultList);

            BigInteger bfAns = CreateFromString(IntListToString(resultList));
            if (sign < 0)
                bfAns.Negate();
            return bfAns;
        }
        /// <summary>Method that is calculating (this * op) for BigInteger objects.
        /// Overrided from parent</summary>
        /// <param name="op">Second operand</param>
        /// <returns>BigInteger equal to (this * op)  upcasted to BigNumber</returns>
        public override BigNumber Multiply(BigNumber op)
        {
            if (!(op is BigInteger))
                throw new ArgumentException("Cannot Add BigInteger and " + op.GetType());

            BigInteger bfLeft = this;
            BigInteger bfRight = (BigInteger)op;
            
            if (bfLeft.CleanString.Length < bfRight.CleanString.Length)
                Swap(ref bfLeft, ref bfRight);
            var leftList = BigIntegerToIntList(bfLeft);
            var rightList = BigIntegerToIntList(bfRight);
            var resultList = leftList.MulWithList(rightList);
            NormalizeList(resultList);

            BigInteger bfAns = CreateFromString(IntListToString(resultList));
            if (bfLeft.Sign * bfRight.Sign < 0)
                bfAns.Negate();
            return bfAns;
        }
        /// <summary>Method that is calculating (this / op) for BigInteger objects.
        /// Overrided from parent</summary>
        /// <param name="op">Second operand</param>
        /// <returns>BigInteger equal to (this / op)  upcasted to BigNumber</returns>
        public override BigNumber Divide(BigNumber op)
        {
            if (!(op is BigInteger))
                throw new ArgumentException("Cannot Add BigInteger to " + op.GetType());

            if (op.CleanString == "0")
                throw new DivideByZeroException();
            BigInteger bfLeft = this;
            BigInteger bfRight = (BigInteger)op;
            
            var leftList = BigIntegerToIntList(bfLeft, 0);
            var rightList = BigIntegerToIntList(bfRight, 0);
            leftList.RemoveTailingZeros();
            rightList.RemoveTailingZeros();

            List<int> resultList = leftList.DivByList(rightList, NormalizeList, out List<int> subList);
            BigInteger bfAns = CreateFromString(IntListToString(resultList));
            if (bfLeft.Sign * bfRight.Sign < 0)
                bfAns.Negate();
            return bfAns;
        }
        /// <summary>Method that is calculating (this % op) for BigInteger objects.
        /// Overrided from parent</summary>
        /// <param name="op">Second operand</param>
        /// <returns>BigInteger equal to (this % op)  upcasted to BigNumber</returns>
        public override BigNumber Mod(BigNumber op)
        {
            if (!(op is BigInteger))
                throw new ArgumentException("Cannot Add BigInteger and " + op.GetType());

            if (op.CleanString == "0")
                throw new ArgumentException("Cannot calculate BigInteger % 0");
            BigInteger bfLeft = this;
            BigInteger bfRight = (BigInteger)op;

            BigInteger bfDiv = bfLeft / bfRight;
            BigInteger bfAns = bfLeft - bfDiv * bfRight;
            return bfAns;
        }       
        /// <summary>Indexer allowing to get indexed digit values</summary>
        /// <param name="index">Integer representing index</param>
        /// <returns>Integer representing digit</rturns>
        public override int this[int index]
        {
            get
            {
                if (index < 0 || index >= CleanString.Length)
                    return -1;
                return ToDigit(CleanString[index]);
            }
        }
        #endregion

        #region Operators
        public static BigInteger operator -(BigInteger num)
        {
            BigInteger ret = new BigInteger(num);

            ret.Negate();
            return ret;
        }

        public static BigInteger operator +(BigInteger left, BigInteger right)
        {
            if (left is null || right is null)
                return null;
            return (BigInteger)left.Add(right);
        }
        public static BigInteger operator -(BigInteger left, BigInteger right)
        {
            if (left is null || right is null)
                return null;
            return (BigInteger)left.Substract(right);
        }
        public static BigInteger operator *(BigInteger left, BigInteger right)
        {
            if (left is null || right is null)
                return null;
            return (BigInteger)left.Multiply(right);
        }
        public static BigInteger operator /(BigInteger left, BigInteger right)
        {
            if (left is null || right is null)
                return null;
            return (BigInteger)left.Divide(right);
        }
        public static BigInteger operator %(BigInteger left, BigInteger right)
        {
            if (left is null || right is null)
                return null;
            return (BigInteger)left.Mod(right);
        }

        public static bool operator >(BigInteger left, BigInteger right)
        {
            if (left.Sign > 0)
            {
                if (right.Sign < 0)
                    return true;
                if (left.CleanString.Length > right.CleanString.Length)
                    return true;
                if (left.CleanString.Length < right.CleanString.Length)
                    return false;
                if (string.Compare(left.CleanString, right.CleanString) > 0)
                    return true;
                return false;
            }
            if (right.Sign > 0)
                return false;
            if (left.CleanString.Length > right.CleanString.Length)
                return false;
            if (left.CleanString.Length < right.CleanString.Length)
                return true;
            if (string.Compare(left.CleanString, right.CleanString) > 0)
                return false;
            return true;
        }
        public static bool operator <(BigInteger left, BigInteger right)
        {
            return (!(left > right));
        }
        public static bool operator ==(BigInteger left, BigInteger right)
        {
            if (string.Compare(left.CleanString, right.CleanString) != 0 
                || left.Sign != right.Sign)
                return false;
            return true;
        }
        public static bool operator !=(BigInteger left, BigInteger right)
        {
            if (string.Compare(left.ToString(), right.ToString()) != 0)
                return false;
            return true;
        }

        public static explicit operator BigDecimal(BigInteger bf)
        {
            return new BigDecimal(bf.CleanString);
        }
        #endregion

        #region Variables
        /// <summary>validStringRegEx is a string representing RegEx
        /// used to validate input string in fabric method <see cref="CreateFromString"/>
        /// into integer and fractional parts </summary>
        private static readonly Regex validStringRegEx = new
            Regex(@"^\s*[+-]?[0-9]+\s*$", RegexOptions.Compiled);
        /// <summary>cleanStringRegEx is a string representing RegEx
        /// used to clean valid input string in fabric method <see cref="CreateFromString"/></summary>
        private static readonly Regex cleanStringRegEx =
            new Regex(@"[1-9]+[0-9]*", RegexOptions.Compiled);
        #endregion
    }
}
