namespace Slade.Commands.Parsing
{
    /// <summary>
    /// Contains information pertaining to a command that has been parsed from command-line arguments.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult"/> class.
        /// </summary>
        /// <param name="value">The raw value for the command.</param>
        public CommandResult(object value)
            : this(string.Empty, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult"/> class.
        /// </summary>
        /// <param name="key">The key value set for the command.</param>
        /// <param name="value">The raw value for the command.</param>
        public CommandResult(string key, object value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Gets a value that indicates whether the command has a value.
        /// </summary>
        public bool HasValue
        {
            get { return Value != null; }
        }

        /// <summary>
        /// Gets the key set for the command, if one exists.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the raw value for the command.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets or sets a value that indicates whether or not the referenced command has been handled by the application.
        /// </summary>
        public bool Handled { get; set; }
    }
}