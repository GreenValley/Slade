using System;

namespace Slade.Commands.Parsing
{
	/// <summary>
	/// Represents a combinable set of valid characters that may be used as a prefix to denote a command key.
	/// </summary>
	[Flags]
	public enum CommandPrefixes
	{
		/// <summary>
		/// Denotes no command prefixes are valid.
		/// </summary>
		None = 0,

		/// <summary>
		/// Represents the Windows-style command prefix of a forward-slash character: /.
		/// </summary>
		ForwardSlash = 1 << 1,

		/// <summary>
		/// Represents the Unix-style command prefix of a single hyphen: -. Usually used in conjunction with
		/// a single character command key or switch.
		/// </summary>
		SingleHyphen = 1 << 2,

		/// <summary>
		/// Represents the Unix-style command prefix of a double hyphen: --. Usually used in conjunction with
		/// a full word command key or switch.
		/// </summary>
		DoubleHyphen = 1 << 3
	}
}