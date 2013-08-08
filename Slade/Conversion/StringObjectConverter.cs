namespace Slade.Conversion
{
    /// <summary>
    /// Performs conversion of boxed objects string representations.
    /// </summary>
    public class StringObjectConverter : ObjectConverter<string>
    {
        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A strongly-typed representation of the given value converted to the supported type.</returns>
        protected override string ConvertCore(object value)
        {
            return value.ToString();
        }
    }
}