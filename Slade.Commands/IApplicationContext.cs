namespace Slade.Commands
{
    /// <summary>
    /// Provides a contract to a context for a running application.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Loads the state of the application context from a backing store.
        /// </summary>
        void Load();

        /// <summary>
        /// Saves the state of the application context into a backing store.
        /// </summary>
        void Save();
    }
}