using System;

namespace MatrixUtilsNetFX
{
    internal static class TextValidationCheckers
    {
        /// <summary>
        /// Checks if all provided strings are not null, empty or whitespace characters
        /// </summary>
        /// <param name="values">Set of strings to be checked</param>
        /// <returns><see langword="true"/> if all strings in <paramref name="values"/> meet the condition and <see langword="false"/> otherwise</returns>
        internal static bool AreAllStringsNotNull(params string[] values)
        {
            foreach (string value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if all provided strings can be parsed into the particular numeric data type
        /// </summary>
        /// <param name="typeCode">The <see cref="TypeCode"/> of the desired numeric data type</param>
        /// <param name="values">Set of strings to be checked</param>
        /// <returns><see langword="true"/> if all strings in <paramref name="values"/> meet the condition and <see langword="false"/> otherwise</returns>
        internal static bool AreAllStringsParsableNumeric(TypeCode typeCode, params string[] values)
        {
            foreach (string value in values)
            {
                switch (typeCode)
                {
                    case TypeCode.Int32:
                        if (!int.TryParse(value, out _)) return false;
                        break;
                    case TypeCode.Decimal:
                        if (!decimal.TryParse(value, out _)) return false;
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if all provided strings, once parsed to numbers, are within the specified range
        /// </summary>
        /// <param name="minValue">The minimum value (inclusive)</param>
        /// <param name="maxValue">The maximum value (inclusive)</param>
        /// <param name="values">Set of strings to be checked</param>
        /// <returns><see langword="true"/> if all strings in <paramref name="values"/> meet the condition and <see langword="false"/> otherwise</returns>
        internal static bool AreAllStringValuesInRange(decimal minValue, decimal maxValue, params string[] values)
        {
            if (AreAllStringsParsableNumeric(TypeCode.Decimal, values) == false) return false;
            foreach (string value in values)
            {
                decimal num = decimal.Parse(value);
                if (num < minValue || num > maxValue) return false;
            }
            return true;
        }
    }
}
