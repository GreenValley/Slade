using System;

namespace Slade.Commands.SimpleCommunicationApplication
{
    /// <summary>
    /// Contains the entry point for the console application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point for the application process execution.
        /// </summary>
        /// <param name="arguments">A collection of arguments provided by a command-line interface.</param>
        public static void Main(string[] arguments)
        {
            try
            {
                var context = new SimpleCommunicationApplicationContext();
                var application = new SimpleCommunicationConsoleApplication(context, arguments);

                application.Run();
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Error, ex.Message);
            }
        }
    }
}
