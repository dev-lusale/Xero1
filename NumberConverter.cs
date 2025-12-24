using System;
using System.Globalization;

namespace Xero1
{
    /// <summary>
    /// Core logic for number base conversions
    /// </summary>
    public static class NumberConverter
    {
        /// <summary>
        /// Converts a number from any base to decimal
        /// </summary>
        public static long ToDecimal(string value, int fromBase)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be empty");

            value = value.Trim().ToUpperInvariant();
            
            // Remove common prefixes
            if (fromBase == 2 && (value.StartsWith("0B") || value.StartsWith("B")))
                value = value.Substring(value.StartsWith("0B") ? 2 : 1);
            else if (fromBase == 8 && value.StartsWith("0"))
                value = value.Substring(1);
            else if (fromBase == 16 && (value.StartsWith("0X") || value.StartsWith("X")))
                value = value.Substring(value.StartsWith("0X") ? 2 : 1);

            try
            {
                return Convert.ToInt64(value, fromBase);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid {GetBaseName(fromBase)} number: {value}", ex);
            }
        }

        /// <summary>
        /// Converts a decimal number to specified base
        /// </summary>
        public static string FromDecimal(long decimalValue, int toBase)
        {
            if (decimalValue == 0) return "0";
            
            return Convert.ToString(decimalValue, toBase).ToUpperInvariant();
        }

        /// <summary>
        /// Direct conversion between any two bases
        /// </summary>
        public static string ConvertBetweenBases(string value, int fromBase, int toBase)
        {
            if (fromBase == toBase) return value;
            
            long decimalValue = ToDecimal(value, fromBase);
            return FromDecimal(decimalValue, toBase);
        }

        /// <summary>
        /// Gets the display name for a base
        /// </summary>
        public static string GetBaseName(int baseValue)
        {
            return baseValue switch
            {
                2 => "Binary",
                8 => "Octal", 
                10 => "Decimal",
                16 => "Hexadecimal",
                _ => $"Base {baseValue}"
            };
        }

        /// <summary>
        /// Validates if a string is valid for the given base
        /// /// </summary>
        public static bool IsValidForBase(string value, int baseValue)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            
            try
            {
                ToDecimal(value, baseValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets formatted output with prefix
        /// </summary>
        public static string GetFormattedOutput(string value, int baseValue, bool includePrefix = true)
        {
            if (!includePrefix) return value;
            
            return baseValue switch
            {
                2 => $"0b{value}",
                8 => $"0{value}",
                16 => $"0x{value}",
                _ => value
            };
        }
    }
}