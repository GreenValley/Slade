using System.Collections.Generic;

namespace Slade.Commands.RunCommandApplication
{
    /// <summary>
    /// Provides a contract for a context for working with instances of the <see cref="RunCommandConsoleApplication"/>
    /// that allows for dependency injection and mocking.
    /// </summary>
    public interface IRunCommandApplicationContext : IApplicationContext
    {
        /// <summary>
        /// Provides access to the collection of program registrations stored within the current context.
        /// </summary>
        Dictionary<string, string> ProgramRegistrations { get; }
    }
}