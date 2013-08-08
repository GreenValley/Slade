using System;

namespace Slade.Commands.RunCommandApplication
{
	/// <summary>
	/// Contains the entry point for the console application.
	/// </summary>
	public static class Program
	{
		private const string APPLICATION_CONTEXT_PATH = @".\run.exe.data";

		/// <summary>
		/// Main entry point for the application process execution.
		/// </summary>
		/// <param name="arguments">A collection of argument provided by a command-line interface.</param>
		public static void Main(string[] arguments)
		{
			try
			{
				var context = new RunCommandApplicationContext(APPLICATION_CONTEXT_PATH);
				var application = new RunCommandConsoleApplication(context, arguments);

				application.Run();
			}
			catch (Exception ex)
			{
				ConsoleHelper.WriteLine(ConsoleMessageType.Error, ex.Message);
			}
		}
	}
}