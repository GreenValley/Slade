using Microsoft.VisualStudio.TestTools.UnitTesting;
using Slade.Commands.Parsing;
using System.Linq;

namespace Slade.Commands.Tests.Parsing
{
	[TestClass]
	public class CommandLineParserTests
	{
		/// <summary>
		/// Configuration:
		///		AllowSwitches		True
		///		Prefixes			DoubleHyphen
		///
		/// Input:
		///		/switch
		///
		/// Output:
		///		1 Command
		///			Key				"switch"
		///			HasValue		False
		/// </summary>
		[TestMethod]
		public void Parse_AllowSwitchesDoubleHyphenPrefix_OneSwitchCommand()
		{
			var parser = new CommandLineParser();
			parser.RuleSet.AllowSwitches = true;
			parser.RuleSet.Prefixes = CommandPrefixes.DoubleHyphen;

			string[] arguments = new string[1] { "--switch" };
			var commands = parser.Parse(arguments).ToArray();

			Assert.AreEqual(1, commands.Length);

			var command = commands[0];
			Assert.AreEqual("switch", command.Key);
			Assert.IsFalse(command.HasValue);
		}

		/// <summary>
		/// Configuration:
		///		Prefixes			ForwardSlash
		///		Separators			Equals
		///
		/// Input:
		///		/key=value
		///
		/// Output:
		///		1 Command
		///			Key				"key"
		///			Value			"value"
		///			HasValue		True
		/// </summary>
		[TestMethod]
		public void Parse_ForwardSlashPrefixEqualsSeparator_OneCommandWithKeyAndValue()
		{
			var parser = new CommandLineParser();
			parser.RuleSet.Prefixes = CommandPrefixes.ForwardSlash;
			parser.RuleSet.Separators = CommandSeparators.Equals;

			string[] arguments = new string[1] { "/key=value" };
			var commands = parser.Parse(arguments).ToArray();

			Assert.AreEqual(1, commands.Length);

			var command = commands[0];
			Assert.AreEqual("key", command.Key);
			Assert.IsTrue(command.HasValue);

			Assert.AreEqual("value", command.Value);
		}

		/// <summary>
		/// Configuration:
		///		AllowMultipleValues	True
		///		Prefixes			ForwardSlash
		///		Separators			Colon
		///
		/// Input:
		///		/key:val1;val2;val3
		///
		/// Output:
		///		1 Command
		///			Key				"key"
		///			Value			["val1", "val2", "val3"]
		///			HasValue		True
		/// </summary>
		[TestMethod]
		public void Parse_AllowMultipleValuesForwardSlashPrefixColonSeparator_OneCommandWithKeyAndThreeValues()
		{
			var parser = new CommandLineParser();
			parser.RuleSet.AllowMultipleValues = true;
			parser.RuleSet.Prefixes = CommandPrefixes.ForwardSlash;
			parser.RuleSet.Separators = CommandSeparators.Colon;

			string[] arguments = new string[1] { "/key:val1;val2;val3" };
			var commands = parser.Parse(arguments).ToArray();

			Assert.AreEqual(1, commands.Length);

			var command = commands[0];
			Assert.AreEqual("key", command.Key);
			Assert.IsTrue(command.HasValue);

			var commandValue = command.Value as string[];
			Assert.IsNotNull(commandValue);
			Assert.AreEqual(3, commandValue.Length);
			Assert.AreEqual("val1", commandValue[0]);
			Assert.AreEqual("val2", commandValue[1]);
			Assert.AreEqual("val3", commandValue[2]);
		}

		/// <summary>
		/// Configuration:
		///		Prefixes			SingleHyphen
		///
		/// Input:
		///		-a -b
		///
		/// Output:
		///		2 Commands
		///			Key				"a"
		///			HasValue		False
		///			Key				"b"
		///			HasValue		False
		/// </summary>
		[TestMethod]
		public void Parse_SingleHyphenPrefix_TwoCommands()
		{
			var parser = new CommandLineParser();
			parser.RuleSet.Prefixes = CommandPrefixes.SingleHyphen;

			string[] arguments = new string[2] { "-a", "-b" };
			var commands = parser.Parse(arguments).ToArray();

			Assert.AreEqual(2, commands.Length);

			var command = commands[0];
			Assert.AreEqual("a", command.Key);
			Assert.IsFalse(command.HasValue);

			command = commands[1];
			Assert.AreEqual("b", command.Key);
			Assert.IsFalse(command.HasValue);
		}
	}
}