using System;

namespace Slade.Commands
{
    /// <summary>
    /// Provides support methods for working with the <see cref="Console"/>.
    /// </summary>
    public static class ConsoleHelper
    {
        private const ConsoleColor INFORMATION_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor ERROR_COLOR = ConsoleColor.Red;

        /// <summary>
        /// Mimics the <see cref="Console.Write(string, object[])"/> method while changing the color of the output message
        /// based on the specified message type.
        /// </summary>
        /// <param name="messageType">The type of message that is being displayed, which dictates the color of the output message.</param>
        /// <param name="format">The message to be displayed that may optionally contain markers for string formatting.</param>
        /// <param name="parameters">An optional collection of parameters to be formatted into the given message format.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given message format is a null reference.</exception>
        public static void Write(ConsoleMessageType messageType, string format, params object[] parameters)
        {
            VerificationProvider.VerifyNotNull(format, "format");

            ChangeConsoleColor(messageType);

            Console.Write(format, parameters);
            Console.ResetColor();
        }

        /// <summary>
        /// Mimics the <see cref="Console.WriteLine(string, object[])"/> method while changing the color of the output message
        /// based on the specified message type.
        /// </summary>
        /// <param name="messageType">The type of message that is being displayed, which dictates the color of the output message.</param>
        /// <param name="format">The message to be displayed that may optionally contain markers for string formatting.</param>
        /// <param name="parameters">An optional collection of parameters to be formatted into the given message format.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given message format is a null reference.</exception>
        public static void WriteLine(ConsoleMessageType messageType, string format, params object[] parameters)
        {
            VerificationProvider.VerifyNotNull(format, "format");

            ChangeConsoleColor(messageType);

            Console.WriteLine(format, parameters);
            Console.ResetColor();
        }

        private static void ChangeConsoleColor(ConsoleMessageType messageType)
        {
            var color = Console.ForegroundColor;

            switch (messageType)
            {
                case ConsoleMessageType.Information:
                    color = INFORMATION_COLOR;
                    break;

                case ConsoleMessageType.Warning:
                    color = WARNING_COLOR;
                    break;

                case ConsoleMessageType.Error:
                    color = ERROR_COLOR;
                    break;
            }

            Console.ForegroundColor = color;
        }
    }
}