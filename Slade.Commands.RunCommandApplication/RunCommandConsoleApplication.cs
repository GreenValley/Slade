using Slade.Commands.Parsing;
using System;
using System.Diagnostics;

namespace Slade.Commands.RunCommandApplication
{
    /// <summary>
    /// Application used to provide a Run command that can be used to register executables and script files
    /// that can be run through the use of this application by specifying the registered name.
    /// </summary>
    public class RunCommandConsoleApplication : ConsoleApplication<IRunCommandApplicationContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommandConsoleApplication" /> class.
        /// </summary>
        /// <param name="applicationContext">An instance of the context required for the current application.</param>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given application context or collection of arguments is null.</exception>
        public RunCommandConsoleApplication(IRunCommandApplicationContext applicationContext, string[] arguments)
            : base(applicationContext, arguments)
        {
            Configure(
                configuration =>
                {
                    configuration.AllowMultipleValues = true;
                    configuration.AllowSwitches = false;
                    configuration.Prefixes = CommandPrefixes.ForwardSlash;
                    configuration.Separators = CommandSeparators.Equals;
                }
            );
        }

        /// <summary>
        /// Registers all supported commands with the console application's registrar.
        /// </summary>
        /// <param name="registrar">A collection of command registrations for the application.</param>
        protected override void RegisterCommands(ExecutableCommandRegistrar registrar)
        {
            base.RegisterCommands(registrar);

            registrar.Register<string[]>("register", HandleRegistration);
            registrar.Register<string>("launch", HandleLaunching);
        }

        private void HandleRegistration(string[] registrationParameters)
        {
            if (registrationParameters.Length != 2)
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Error,
                                        "Invalid number of values specified for command registration. Please specify the name and program path.");

                return;
            }

            // Extract the registration name and program path from the registration parameters
            string registrationName = registrationParameters[0];
            string programPath = registrationParameters[1];

            VerificationProvider.VerifyValidString(registrationName, "registrationName");
            VerificationProvider.VerifyValidString(programPath, "programPath");

            // Check we haven't already made a registration with this name
            if (ApplicationContext.ProgramRegistrations.ContainsKey(registrationName))
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Warning,
                                        "A program has already been registered under the name '{0}' and will be overridden.",
                                        registrationName);
            }

            // Store the registration, overriding any existing registrations
            ApplicationContext.ProgramRegistrations[registrationName] = programPath;

            ConsoleHelper.WriteLine(ConsoleMessageType.Information,
                                    "A registration has been successfully made under '{0}' for the path '{1}'.",
                                    registrationName, programPath);
        }

        private void HandleLaunching(string registrationName)
        {
            string programPath;
            if (!ApplicationContext.ProgramRegistrations.TryGetValue(registrationName, out programPath))
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Warning, "No registration exists under the name '{0}'.",
                                        registrationName);
            }

            // Try to launch a new process using the program path registered under the name
            Process.Start(programPath);
        }
    }
}