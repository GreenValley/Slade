using System;
using System.Collections.Generic;

namespace Slade.Commands.Parsing
{
    /// <summary>
    /// Handles parsing of arguments supplied by a command-line interface based on a given set of parsing rules.
    /// </summary>
    public sealed class CommandLineParser
    {
        private static readonly string[] MultipleValuesSeparators = new string[1] { ";" };

        private readonly CommandLineRuleSet mRuleSet = CommandLineRuleSet.WindowsProfile;

        /// <summary>
        /// Provides access to the rule set that will be used when performing a parse operation.
        /// </summary>
        public CommandLineRuleSet RuleSet
        {
            get { return mRuleSet; }
        }

        /// <summary>
        /// Parses the given collection of arguments based on the current parser rule set.
        /// </summary>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <returns>A collection of commands resulting from the parse operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the given collection of arguments is null.</exception>
        public IEnumerable<CommandResult> Parse(string[] arguments)
        {
            VerificationProvider.VerifyNotNull(arguments, "arguments");

            var context = new CommandParsingContext(RuleSet);
            foreach (string argument in arguments)
            {
                var command = Parse(context, argument);
                if (command != null)
                {
                    yield return command;
                }
            }
        }

        private CommandResult Parse(CommandParsingContext context, string argument)
        {
            if (IsCommand(context, argument))
            {
                string commandKey = ExtractCommandKey(context, argument);
                object commandValue = ExtractCommandValue(context, argument);

                return new CommandResult(commandKey, commandValue);
            }

            return null;
        }

        private bool IsCommand(CommandParsingContext context, string argument)
        {
            var prefixes = context.GetPrefixes();
            return argument.StartsWithAny(prefixes);
        }

        private string ExtractCommandKey(CommandParsingContext context, string command)
        {
            var prefixes = context.GetPrefixes();
            var separators = context.GetSeparators();

            // Trim off any prefix characters from the start of the command and split on the first occurrence of any separator characters
            string commandKey = command.TrimStart(prefixes);
            int splitIndex = commandKey.IndexOfAny(separators);

            if (splitIndex > -1 && splitIndex < commandKey.Length)
            {
                commandKey = commandKey.Substring(0, splitIndex);
            }

            return commandKey;
        }

        private object ExtractCommandValue(CommandParsingContext context, string command)
        {
            var separators = context.GetSeparators();

            // Split the command from the first occurrence of any separator characters
            int splitIndex = command.IndexOfAny(separators);

            if (splitIndex < 0 || splitIndex >= command.Length)
            {
                return null;
            }

            string commandValue = command.Substring(splitIndex + 1);

            // Check to see if we have multiple values
            if (commandValue.Contains(MultipleValuesSeparators[0]))
            {
                return commandValue.Split(MultipleValuesSeparators, StringSplitOptions.RemoveEmptyEntries);
            }

            // We have only a single value
            return commandValue;
        }
    }
}