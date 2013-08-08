using System;

namespace Slade.Conversion
{
    /// <summary>
    /// Performs conversion of objects that can be represented as a string array.
    /// </summary>
    public class StringArrayObjectConverter : ObjectConverter<string[]>
    {
        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A strongly-typed representation of the given value converted to the supported type.</returns>
        protected override string[] ConvertCore(object value)
        {
            // If we get to this point, the given value isn't a string array already so we can't do anything with it
            throw new NotSupportedException();
        }
    }
}