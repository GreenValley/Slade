using System;
using System.Collections.Generic;
using System.Linq;

namespace Slade.Commands.Parsing
{
    /// <summary>
    /// Contains information pertinent to the context of performing command parsing operations.
    /// </summary>
    public sealed class CommandParsingContext
    {
        private static readonly Dictionary<CommandPrefixes, string> sCommandPrefixCharacters = new Dictionary<CommandPrefixes, string>();
        private static readonly Dictionary<CommandSeparators, string> sCommandSeparatorCharacters = new Dictionary<CommandSeparators, string>();

        /// <summary>
        /// Initializes static instances contained within the <see cref="CommandParsingContext"/> class.
        /// </summary>
        static CommandParsingContext()
        {
            sCommandPrefixCharacters[CommandPrefixes.ForwardSlash] = "/";
            sCommandPrefixCharacters[CommandPrefixes.SingleHyphen] = "-";
            sCommandPrefixCharacters[CommandPrefixes.DoubleHyphen] = "--";

            sCommandSeparatorCharacters[CommandSeparators.Colon] = ":";
            sCommandSeparatorCharacters[CommandSeparators.Equals] = "=";
        }

        private readonly CommandLineRuleSet mRuleSet;

        private string[] mPrefixes;
        private string[] mSeparators;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParsingContext"/> class.
        /// </summary>
        /// <param name="ruleSet">The rule set in use by the command parsing operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given rule set is null.</exception>
        internal CommandParsingContext(CommandLineRuleSet ruleSet)
        {
            VerificationProvider.VerifyNotNull(ruleSet, "ruleSet");

            mRuleSet = ruleSet;
        }

        /// <summary>
        /// Retrieves a read-only collection of valid command prefix characters as described
        /// by the encapsulated rule set.
        /// </summary>
        /// <returns>A collection of valid command prefix characters.</returns>
        /// <remarks>
        /// <para>
        /// Upon first access of this method, the prefixes will be loaded and cached internally.
        /// To force a reload of the prefix values (in the case that the rule set has changed),
        /// the <see cref="Flush()"/> method must be called.
        /// </para>
        /// </remarks>
        public string[] GetPrefixes()
        {
            if (mPrefixes == null)
            {
                var prefixValues = EnumHelper.ExtractFlagValues<CommandPrefixes>(mRuleSet.Prefixes, excludeZeroValue: true).ToArray();
                mPrefixes = new string[prefixValues.Length];

                for (int i = 0; i < prefixValues.Length; i++)
                {
                    mPrefixes[i] = sCommandPrefixCharacters[prefixValues[i]];
                }
            }

            return mPrefixes;
        }

        /// <summary>
        /// Retrieves a read-only collection of valid command key/value separator characters
        /// as described by the encapsulated rule set.
        /// </summary>
        /// <returns>A collection of valid command key/value separator characters.</returns>
        /// <remarks>
        /// <para>
        /// Upon first access of this method, the separators will be loaded and cached internally.
        /// To force a reload of the separator values (in the case that the rule set has changed),
        /// the <see cref="Flush()"/> method must be called.
        /// </para>
        /// </remarks>
        public string[] GetSeparators()
        {
            if (mSeparators == null)
            {
                var separatorValues = EnumHelper.ExtractFlagValues<CommandSeparators>(mRuleSet.Separators, excludeZeroValue: true).ToArray();
                mSeparators = new string[separatorValues.Length];

                for (int i = 0; i < separatorValues.Length; i++)
                {
                    mSeparators[i] = sCommandSeparatorCharacters[separatorValues[i]];
                }
            }

            return mSeparators;
        }

        /// <summary>
        /// Flushes all cached values from the context to allow for reloading of the data upon access.
        /// </summary>
        public void Flush()
        {
            mPrefixes = null;
            mSeparators = null;
        }
    }
}