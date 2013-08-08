using System;
using System.Collections.Generic;

namespace Slade.Conversion
{
    /// <summary>
    /// Handles creation of <see cref="IObjectConverter"/> implementations based on requested types.
    /// </summary>
    public sealed class ObjectConverterFactory
    {
        private readonly Dictionary<Type, ObjectConverterRegistration> mConverters =
            new Dictionary<Type, ObjectConverterRegistration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectConverterFactory"/> class.
        /// </summary>
        public ObjectConverterFactory()
        {
            // Set up all known object converters
            Register<string, StringObjectConverter>();
            Register<string[], StringArrayObjectConverter>();
        }

        /// <summary>
        /// Registers a custom <see cref="IObjectConverter"/> implementation under the specified target type.
        /// </summary>
        /// <typeparam name="TTarget">The type of object conversion supported by the converter.</typeparam>
        /// <typeparam name="TConverter">The type of <see cref="IObjectConverter"/> implementation.</typeparam>
        /// <remarks>
        /// <para>
        /// An instance of the specified <see cref="IObjectConverter"/> implementation will only be created
        /// once the converter is requested for creation.
        /// </para>
        /// <para>
        /// If a converter has already been registered for the given target type, the current registration
        /// will override the registration that already exists.
        /// </para>
        /// </remarks>
        public void Register<TTarget, TConverter>()
            where TConverter : IObjectConverter<TTarget>, new()
        {
            // Set up a new converter registration
            var registration = new ObjectConverterRegistration(typeof(TTarget), () => new TConverter());

            // Store the registration and make sure any existing registrations are overridden
            mConverters[registration.TargetType] = registration;
        }

        /// <summary>
        /// Creates an instance of the requested object converter for the specified type.
        /// </summary>
        /// <typeparam name="TTarget">The type of conversion a converter is required for.</typeparam>
        /// <returns>An instance of the requested object converter that handles conversion to the
        /// specified target object type.</returns>
        /// <exception cref="NotSupportedException">Thrown when no converter exists that supports conversion
        /// of objects to the specified target type.</exception>
        /// <remarks>
        /// <para>
        /// If an instance of the requested object converter has already been created, it will be cached and
        /// re-used for each creation request. All <see cref="IObjectConverter" /> implementations should be stateless.
        /// </para>
        /// </remarks>
        public IObjectConverter<TTarget> Create<TTarget>()
        {
            ObjectConverterRegistration registration;

            // Check for a registration under the specified type
            var targetType = typeof(TTarget);
            if (!mConverters.TryGetValue(targetType, out registration) || registration == null)
            {
                throw new NotSupportedException(
                    string.Format("No converters are registered that support conversion of objects to the type '{0}'.",
                                  targetType.FullName));
            }

            // Retrieve the strongly-typed object converter instance from the registration
            return registration.GetInstance<TTarget>();
        }

        private sealed class ObjectConverterRegistration
        {
            private readonly Func<IObjectConverter> mObjectCreator;
            private IObjectConverter mConverter;

            /// <summary>
            /// Initializes a new instance of the <see cref="ObjectConverterRegistration"/> class.
            /// </summary>
            /// <param name="targetType">The type of conversion supported by the registered converter.</param>
            /// <param name="objectCreator">A function used to create an instance of the registered
            /// <see cref="IObjectConverter"/> implementation.</param>
            /// <exception cref="ArgumentNullException">Thrown when the given target type or object 
            /// creation function is null.</exception>
            internal ObjectConverterRegistration(Type targetType, Func<IObjectConverter> objectCreator)
            {
                VerificationProvider.VerifyNotNull(objectCreator, "objectCreator");

                TargetType = targetType;
                mObjectCreator = objectCreator;
            }

            /// <summary>
            /// Gets the type of conversion supported by the registered converter.
            /// </summary>
            public Type TargetType { get; private set; }

            /// <summary>
            /// Retrieves the instance of the registered <see cref="IObjectConverter"/> implementation
            /// in a strongly-typed form based on the specified target type.
            /// </summary>
            /// <typeparam name="TTarget">The type of object conversion expected for the converter to support.</typeparam>
            /// <returns>An instance of the requested converter registration.</returns>
            /// <exception cref="InvalidOperationException">Thrown when the given object creation function fails
            /// to return an instance of the registered <see cref="IObjectConverter"/> implementation.</exception>
            /// <exception cref="NotSupportedException">Thrown when the requested conversion type is not supported
            /// by the registered <see cref="IObjectConverter"/> implementation.</exception>
            /// <remarks>
            /// <para>
            /// If the <see cref="IObjectConverter"/> implementation has not yet been instantiated, it will
            /// first be created and then cached for later access.
            /// </para>
            /// </remarks>
            public IObjectConverter<TTarget> GetInstance<TTarget>()
            {
                // Check that we are requesting the correct converter instance
                var requestedType = typeof(TTarget);
                if (requestedType != TargetType)
                {
                    throw new NotSupportedException(
                        string.Format(
                            "The registered converter supports the type '{0}' but the type '{1}' was requested.",
                            requestedType.FullName, TargetType.FullName));
                }

                if (mConverter == null)
                {
                    // Invoke the object creator to instantiate the converter
                    mConverter = mObjectCreator.Invoke();

                    if (mConverter == null)
                    {
                        throw new InvalidOperationException("The object creator failed to instantiate the converter.");
                    }
                }

                // Now that we have a converter instance, attempt direct conversion to the specified type
                return (IObjectConverter<TTarget>)mConverter;
            }
        }
    }
}