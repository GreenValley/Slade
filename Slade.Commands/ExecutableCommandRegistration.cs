using Slade.Commands.Parsing;
using Slade.Conversion;
using System;

namespace Slade.Commands
{
    /// <summary>
    /// Provides a contract to a command registration that can be executed within the context of a console application.
    /// </summary>
    public interface IExecutableCommandRegistration
    {
        /// <summary>
        /// Gets the name of the command used for registration.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Executes the registered command by invoking the contained action.
        /// </summary>
        /// <param name="result">The result of the command parsing process pertinent to the registered command.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given command result is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when invocation of the contained action fails.</exception>
        void Execute(CommandResult result);
    }

    /// <summary>
    /// Contains information pertaining to a command registered as being available within the a console application.
    /// </summary>
    /// <typeparam name="TValue">The type of value expected to be handled by the execution action.</typeparam>
    public sealed class ExecutableCommandRegistration<TValue> : IExecutableCommandRegistration
    {
        private readonly ObjectConverterFactory mObjectConverterFactory;

        private readonly string mName;
        private readonly Action<TValue> mExecutionAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutableCommandRegistration{TValue}"/> class.
        /// </summary>
        /// <typeparam name="TValue">The type of value expected to be handled by the execution action.</typeparam>
        /// <param name="name">The name of the command to be registered.</param>
        /// <param name="executionAction">The action to be invoked when the command is triggered.</param>
        /// <param name="objectConverterFactory">A factory used to create requested object converters.</param>
        /// <exception cref="ArgumentException">Thrown when the given name is an invalid string.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the given or execution action object converter factory is null.</exception>
        internal ExecutableCommandRegistration(string name, Action<TValue> executionAction, ObjectConverterFactory objectConverterFactory)
        {
            VerificationProvider.VerifyValidString(name, "name");
            VerificationProvider.VerifyNotNull(executionAction, "executionAction");
            VerificationProvider.VerifyNotNull(objectConverterFactory, "objectConverterFactory");

            mName = name;
            mExecutionAction = executionAction;
            mObjectConverterFactory = objectConverterFactory;
        }

        /// <summary>
        /// Gets the name of the command used for registration.
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        /// <summary>
        /// Executes the registered command by invoking the contained action.
        /// </summary>
        /// <param name="result">The result of the command parsing process pertinent to the registered command.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given command result is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when invocation of the contained action fails.</exception>
        public void Execute(CommandResult result)
        {
            VerificationProvider.VerifyNotNull(result, "result");

            // Get hold of the relevant converter for the command value type
            var converter = mObjectConverterFactory.Create<TValue>();
            TValue value = converter.Convert(result.Value);

            try
            {
                mExecutionAction.Invoke(value);
            }
            catch (Exception ex)
            {
                // Wrap the exception within an InvalidOperationException and re-throw
                throw new InvalidOperationException(
                    "An error occurred while executing the command. See inner exception for details.", ex);
            }
        }
    }
}