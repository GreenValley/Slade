using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Slade.Commands.RunCommandApplication;
using System;
using System.Collections.Generic;

namespace Slade.Commands.Applications.Tests
{
    [TestClass]
    public class RunCommandConsoleApplicationTests
    {
        /// <summary>
        /// Command-Line Arguments:
        ///     "/register=single;C:\path\to\program.exe"
        ///     
        /// Commands:
        ///     [0]: Key    "register"
        ///          Values ["single", "C:\path\to\program.exe"]
        /// 
        /// Registrations:
        ///     [0]: Name   "single"
        ///          Path   "C:\path\to\program.exe"
        /// </summary>
        [TestMethod]
        public void Run_RegisterSingleProgram_SuccessfulRegistration()
        {
            string[] arguments = new string[1] { @"/register=single;C:\path\to\program.exe" };
            var registrations = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            var applicationContext = new Mock<IRunCommandApplicationContext>();
            applicationContext.SetupGet(context => context.ProgramRegistrations)
                              .Returns(registrations);

            var application = new RunCommandConsoleApplication(applicationContext.Object, arguments);
            application.Run();

            Assert.AreEqual(1, registrations.Count);
            Assert.IsTrue(registrations.ContainsKey("single"));
            Assert.AreEqual(@"C:\path\to\program.exe", registrations["single"]);
        }

        /// <summary>
        /// Existing Registrations:
        ///     [0]: Name   "single"
        ///          Path   "C:\path\to\program.exe"
        /// 
        /// Command-Line Arguments:
        ///     [0]: Key    "register"
        ///          Values ["single", "C:\i\have\been\changed.exe"]
        /// 
        /// Registrations:
        ///     [0]: Name   "single"
        ///          Path   "C:\i\have\been\changed.exe"
        /// </summary>
        [TestMethod]
        public void Run_OverrideProgramRegistration_SuccessfulRegistration()
        {
            string[] arguments = new string[1] { @"/register=single;C:\i\have\been\changed.exe" };
            var registrations = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            registrations["single"] = @"C:\path\to\program.exe";

            var applicationContext = new Mock<IRunCommandApplicationContext>();
            applicationContext.SetupGet(context => context.ProgramRegistrations)
                              .Returns(registrations);

            var application = new RunCommandConsoleApplication(applicationContext.Object, arguments);
            application.Run();

            Assert.AreEqual(1, registrations.Count);
            Assert.IsTrue(registrations.ContainsKey("single"));
            Assert.AreEqual(@"C:\i\have\been\changed.exe", registrations["single"]);
        }
    }
}