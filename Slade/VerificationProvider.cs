using System;

namespace Slade
{
    /// <summary>
    /// Contains static methods used to perform simple verification on method parameters
    /// and throw appropriate exceptions when conditions are not satisfied.
    /// </summary>
    public static class VerificationProvider
    {
        /// <summary>
        /// Checks that the given parameter is not a null reference and throws an instance
        /// of the <see cref="ArgumentNullException" /> class if it is.
        /// </summary>
        /// <param name="parameter">The parameter value to be checked.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        public static void VerifyNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Checks that the given string value parameter is valid in terms of not being null or empty
        /// and throws an instance of the <see cref="ArgumentException"/> class if it is not valid.
        /// </summary>
        /// <param name="parameter">The string value parameter to verify.</param>
        /// <param name="parameterName">The name of the parameter being verified.</param>
        /// <param name="allowWhitespace">An optional value that indicates whether the string may be deemed valid if it
        /// contains only whitespace characters. The default is false.</param>
        public static void VerifyValidString(string parameter, string parameterName, bool allowWhitespace = false)
        {
            if ((allowWhitespace && string.IsNullOrEmpty(parameter))
                || (!allowWhitespace && string.IsNullOrWhiteSpace(parameter)))
            {
                throw new ArgumentException(parameterName);
            }
        }
    }
}