using System;

namespace Slade.Conversion
{
    /// <summary>
    /// Provides an abstract implementation of an <see cref="IObjectConverter{TTarget}"/> implementation.
    /// </summary>
    /// <typeparam name="TTarget">The type of object the converter supports as a target.</typeparam>
    public abstract class ObjectConverter<TTarget> : IObjectConverter<TTarget>
    {
        private readonly Type mTargetType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectConverter{TTarget}"/> class.
        /// </summary>
        protected ObjectConverter()
        {
            mTargetType = typeof(TTarget);
        }

        /// <summary>
        /// The type of object conversion supported by this converter.
        /// </summary>
        public Type TargetType
        {
            get { return mTargetType; }
        }

        /// <summary>
        /// Determines whether the given object type is supported by this converter.
        /// </summary>
        /// <param name="type">The type of the object requiring conversion.</param>
        /// <returns></returns>
        public bool Supports(Type type)
        {
            return TargetType == type;
        }

        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A boxed representation of the given value converted to the supported type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the given value is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the given value cannot be converted to the target type.</exception>
        object IObjectConverter.Convert(object value)
        {
            return Convert(value);
        }

        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A strongly-typed representation of the given value converted to the supported type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the given value is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the given value cannot be converted to the target type.</exception>
        public TTarget Convert(object value)
        {
            VerificationProvider.VerifyNotNull(value, "value");

            // Attempt a direct cast to the target type
            if (value is TTarget)
            {
                return (TTarget)value;
            }

            return ConvertCore(value);
        }

        /// <summary>
        /// Converts the given object value to an instance of the supported type.
        /// </summary>
        /// <param name="value">The object value to be converted.</param>
        /// <returns>A strongly-typed representation of the given value converted to the supported type.</returns>
        protected abstract TTarget ConvertCore(object value);
    }
}