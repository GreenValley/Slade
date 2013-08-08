using System;
using System.Collections.Generic;

namespace Slade
{
	/// <summary>
	/// Provides support for working with enumerations.
	/// </summary>
	public static class EnumHelper
	{
		/// <summary>
		/// Retrieves a collection of all values of the specified enumeration that are contained within
		/// the single flags value.
		/// </summary>
		/// <typeparam name="T">The type of enumeration to get the values for.</typeparam>
		/// <param name="enumValue">The single flags value of the enumeration.</param>
		/// <param name="excludeZeroValue">Specifies whether to exclude enumeration flag values that equate to zero.</param>
		/// <returns>A collection of all values set within the given enumeration value.</returns>
		/// <exception cref="ArgumentException">Thrown when the specified type is not of an enumeration.</exception>
		public static IEnumerable<T> ExtractFlagValues<T>(T enumValue, bool excludeZeroValue)
			where T : struct
		{
			var enumType = typeof(T);
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("The specified type is not of an enumeration.");
			}

			// Cycle over each value contained within the specified enumeration and determine which are contained within given value
			// Note: We need to box and unbox the enum values to get them into Int32 formats, so we can perform bitwise operations on them.
			int rawEnumValue = (int)(object)enumValue;
			foreach (T value in (IEnumerable<T>)Enum.GetValues(enumType))
			{
				int rawValue = (int)(object)value;

				if (((rawEnumValue & rawValue) == rawValue) && (!excludeZeroValue || rawValue != 0))
				{
					yield return value;
				}
			}
		}
	}
}