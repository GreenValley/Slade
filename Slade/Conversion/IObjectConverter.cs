using System;
namespace Slade.Conversion
{
    /// <summary>
    /// Provides a common contract to a converter of objects.
    /// </summary>
    public interface IObjectConverter
    {
        /// <summary>
        /// The type of object conversion supported by this converter.
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// Determines whether the given object type is supported by this converter.
        /// </summary>
        /// <param name="type">The type of the object requiring conversion.</param>
        /// <returns></returns>
        bool Supports(Type type);

        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A boxed representation of the given value converted to the supported type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the given value is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the given value cannot be converted to the target type.</exception>
        object Convert(object value);
    }

    /// <summary>
    /// Provides a contract to a converter of objects to specific target types.
    /// </summary>
    /// <typeparam name="TTarget">The type of object the converter supports as a target.</typeparam>
    public interface IObjectConverter<out TTarget> : IObjectConverter
    {
        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A strongly-typed representation of the given value converted to the supported type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the given value is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the given value cannot be converted to the target type.</exception>
        new TTarget Convert(object value);
    }
}