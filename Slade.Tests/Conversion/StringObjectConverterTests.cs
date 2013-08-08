using Microsoft.VisualStudio.TestTools.UnitTesting;
using Slade.Conversion;

namespace Slade.Tests.Conversion
{
    [TestClass]
    public class StringObjectConverterTests
    {
        [TestMethod]
        public void Convert_StringObjectValue_DirectCastApplied()
        {
            var converter = new StringObjectConverter();
            string result = converter.Convert("SomeStringValue");

            Assert.AreEqual("SomeStringValue", result);
        }

        [TestMethod]
        public void Convert_IntegerObjectValue_ToStringApplied()
        {
            var converter = new StringObjectConverter();
            string result = converter.Convert(1.234d);

            Assert.AreEqual("1.234", result);
        }
    }
}