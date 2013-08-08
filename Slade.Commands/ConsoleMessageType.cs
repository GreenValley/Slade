namespace Slade.Commands
{
    /// <summary>
    /// Represents the different types of messages that can be output to the console.
    /// </summary>
    public enum ConsoleMessageType
    {
        /// <summary>
        /// Refers to an informative message.
        /// </summary>
        Information,

        /// <summary>
        /// Refers to a message that highlights a warning.
        /// </summary>
        Warning,

        /// <summary>
        /// Refers to a critical error message.
        /// </summary>
        Error
    }
}