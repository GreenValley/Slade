using Slade.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Slade.Commands.RunCommandApplication
{
	/// <summary>
	/// Implements the <see cref="IApplicationContext"/> interface to provide a context specific to working
	/// with a running instance of the <see cref="RunCommandConsoleApplication"/> class.
	/// </summary>
	public sealed class RunCommandApplicationContext : IRunCommandApplicationContext
	{
		private static readonly Type SerializationType = typeof(List<KeyValuePair<string, string>>);
		private readonly XmlSerializer mSerializer = new XmlSerializer(SerializationType);

		private readonly string mFilePath;

		private readonly Dictionary<string, string> mProgramRegistrations =
			new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

		/// <summary>
		/// Initializes a new instance of the <see cref="RunCommandApplicationContext"/> class.
		/// </summary>
		/// <param name="filePath">The path to which the context information should be written to and read from.</param>
		/// <exception cref="ArgumentException">Thrown when the given file path is not a valid string.</exception>
		public RunCommandApplicationContext(string filePath)
		{
			VerificationProvider.VerifyValidString(filePath, "filePath");

			mFilePath = filePath;

			// Make sure the directory exists
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
		}

		/// <summary>
		/// Provides access to the collection of program registrations stored within the current context.
		/// </summary>
		public Dictionary<string, string> ProgramRegistrations
		{
			get { return mProgramRegistrations; }
		}

		/// <summary>
		/// Loads the state of the application context from a backing store.
		/// </summary>
		public void Load()
		{
			if (!File.Exists(mFilePath))
			{
				// The file doesn't exist so we have nothing to read
				return;
			}

			// De-serialize the application context information from the specified path
			using (var stream = File.OpenRead(mFilePath))
			{
				using (var reader = new BinaryReader(stream))
				{
					var registrations = ReadBinaryEntries(reader);
					mProgramRegistrations.Initialize<string, string>(registrations);
				}
			}
		}

		private static IEnumerable<KeyValuePair<string, string>> ReadBinaryEntries(BinaryReader reader)
		{
			// Begin by reading the number of entries from the stream
			int entryCount = reader.ReadInt32();

			// Read and yield each entry from the stream
			while (entryCount-- > 0)
			{
				string key = reader.ReadString();
				string value = reader.ReadString();

				yield return new KeyValuePair<string, string>(key, value);
			}
		}

		/// <summary>
		/// Saves the state of the application context into a backing store.
		/// </summary>
		public void Save()
		{
			// Serialize the application context information into the specified path
			using (var stream = File.OpenWrite(mFilePath))
			{
				stream.SetLength(0L);

				using (var writer = new BinaryWriter(stream))
				{
					// Begin by writing the number of entries to the stream
					writer.Write(mProgramRegistrations.Count);

					// Write each entry into the stream
					foreach (var registration in mProgramRegistrations)
					{
						writer.Write(registration.Key);
						writer.Write(registration.Value);
					}
				}
			}
		}
	}
}