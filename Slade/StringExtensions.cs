using System;
using System.Collections.Generic;
using System.Linq;

namespace Slade
{
	/// <summary>
	/// Provides functionality to extend the <see cref="String"/> class.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Determines whether the source string starts with any of the given string values.
		/// </summary>
		/// <param name="source">The source string to test the start of.</param>
		/// <param name="values">The string values to check the start of the source string for.</param>
		/// <returns>True if the source string starts with any of the given string values.</returns>
		/// <exception cref="ArgumentNullException">Thrown when either the source string or given collection
		/// of string values is null.</exception>
		public static bool StartsWithAny(this string source, IEnumerable<string> values)
		{
			VerificationProvider.VerifyNotNull(source, "source");
			VerificationProvider.VerifyNotNull(values, "values");

			foreach (string value in values)
			{
				if (source.StartsWith(value))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Trims any of the given string values that are present at the start of the source string.
		/// </summary>
		/// <param name="values">A collection of string values to be removed if they exist at the start of the source string.</param>
		/// <returns>The resulting source string after any trimming.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the </exception>
		/// <remarks>
		/// <para>
		/// Only one of the given values will be trimmed from the start of the source string. Once this value has been
		/// trimmed, the process will exit.
		/// </para>
		/// </remarks>
		public static string TrimStart(this string source, string[] values)
		{
			foreach (string value in values)
			{
				if (source.StartsWith(value))
				{
					return source.TrimStart(value.ToCharArray());
				}
			}

			// None of the given values were found at the start of the source string
			return source;
		}

		/// <summary>
		/// Finds the index of the first occurence of any of the given string values within the source string.
		/// </summary>
		/// <param name="source">The string in which to find any of the given string values.</param>
		/// <param name="values">A collection of values to find within the source string.</param>
		/// <returns>The index in the source string of any of the given string values.</returns>
		/// <remarks>
		/// <para>
		/// Each of the given string values will be checked and the value that occurs first within the source
		/// string will have its index value returned.
		/// </para>
		/// </remarks>
		public static int IndexOfAny(this string source, string[] values)
		{
			var indexes = new HashSet<int>();

			foreach (string value in values)
			{
				indexes.Add(source.IndexOf(value));
			}

			if (indexes.Count == 0)
			{
				// None of the given string values are contained within the source string
				return -1;
			}

			return indexes.OrderBy(x => x).First();
		}
	}
}