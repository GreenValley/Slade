using Slade.Conversion;
using System;
using System.Collections.Generic;

namespace Slade.Commands.Parsing
{
	/// <summary>
	/// Contains an enumerable set of <see cref="CommandResult"/> instances, while also providing functions
	/// for easy interaction with required commands.
	/// </summary>
	public sealed class CommandResultSet : IEnumerable<CommandResult>
	{
		private readonly ObjectConverterFactory mObjectConverterFactory;
		private readonly IEnumerable<CommandResult> mCommands;

		private readonly Dictionary<string, CommandResult> mKeyedCommands =
			new Dictionary<string, CommandResult>(StringComparer.InvariantCultureIgnoreCase);

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandResultSet"/> class.
		/// </summary>
		/// <param name="objectConverterFactory">A factory used to create requested object converters.</param>
		/// <param name="commands">A collection of commands to add to the set.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given object converter factory or collection of commands is null.</exception>
		internal CommandResultSet(ObjectConverterFactory objectConverterFactory, IEnumerable<CommandResult> commands)
		{
			VerificationProvider.VerifyNotNull(objectConverterFactory, "objectConverterFactory");
			VerificationProvider.VerifyNotNull(commands, "commands");

			mObjectConverterFactory = objectConverterFactory;
			mCommands = commands;

			// Extract all commands that have a key value
			foreach (var command in commands)
			{
				mKeyedCommands[command.Key] = command;
			}
		}

		/// <summary>
		/// Attempts to retrieve a value from the set of commands that is stored under the given key.
		/// </summary>
		/// <typeparam name="TValue">The type of the value to be retrieved.</typeparam>
		/// <param name="key">The key of the command to retrieve.</param>
		/// <param name="value">An output parameter that will be populated with the requested value.</param>
		/// <returns>True if the given key matches a stored command; false otherwise.</returns>
		/// <exception cref="ArgumentException">Thrown when the given key value is not a valid string.</exception>
		public bool TryGetValue<TValue>(string key, out TValue value)
		{
			VerificationProvider.VerifyValidString(key, "key");

			// Initialize the output parameter with a default value
			value = default(TValue);

			CommandResult command;
			if (!mKeyedCommands.TryGetValue(key, out command))
			{
				// The command does not exist under the given key
				return false;
			}

			// We have the command, so get hold of the relevant converter for its type
			var converter = mObjectConverterFactory.Create<TValue>();
			value = converter.Convert(command.Value);

			return true;
		}

		/// <summary>
		/// Gets a strongly-typed enumerator instance used to enumerate the collection of <see cref="CommandResult"/> instances.
		/// </summary>
		/// <returns>The strongly-typed enumerator instance.</returns>
		public IEnumerator<CommandResult> GetEnumerator()
		{
			return mCommands.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator instance used to enumerate the collection of <see cref="CommandResult"/> instances.
		/// </summary>
		/// <returns>The requested enumerator instance.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}