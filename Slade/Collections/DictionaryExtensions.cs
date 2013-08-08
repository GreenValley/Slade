using System;
using System.Collections.Generic;

namespace Slade.Collections
{
	/// <summary>
	/// Provides functionality to extend the <see cref="IDictionary{TKey, TValue}"/> interface.
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Performs initialization on the given dictionary instance by removing all existing items and replacing them with all items contained
		/// within the given collection of key-value pairs.
		/// </summary>
		/// <typeparam name="TKey">The type of the key objects contained within the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the value objects contained within the dictionary.</typeparam>
		/// <param name="dictionary">The dictionary instance to be re-initialized.</param>
		/// <param name="collection">A collection of key-value pairs containing the items to be added to the dictionary.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given dictionary or collection is null.</exception>
		public static void Initialize<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			VerificationProvider.VerifyNotNull(dictionary, "dictionary");
			VerificationProvider.VerifyNotNull(collection, "collection");

			dictionary.Clear();

			foreach (var item in collection)
			{
				if (item.Key == null || item.Value == null)
				{
					// Invalid key/value parameters
					continue;
				}

				dictionary.Add(item);
			}
		}
	}
}