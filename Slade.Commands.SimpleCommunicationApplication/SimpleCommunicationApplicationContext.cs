namespace Slade.Commands.SimpleCommunicationApplication
{
    /// <summary>
    /// Implements the <see cref="ISimpleCommunicationApplicationContext"/> interface to provide a context specific
    /// to working with a running instance of the <see cref="SimpleCommunicationConsoleApplication"/> class.
    /// </summary>
    public sealed class SimpleCommunicationApplicationContext : ISimpleCommunicationApplicationContext
    {
        public void Load()
        {
            // This is currently not supported as we have no information that requires persisting
        }

        public void Save()
        {
            // This is currently not supported as we have no information that requires persisting
        }
    }
}