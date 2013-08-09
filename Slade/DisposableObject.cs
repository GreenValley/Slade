using System;

namespace Slade
{
    /// <summary>
    /// Represents an object that employs the proper disposable pattern for releasing object resources.
    /// </summary>
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.71).aspx"/>
    public abstract class DisposableObject : IDisposable
    {
        ~DisposableObject()
        {
            // Note: We are explicitly calling the Dispose method from within the Finalizer
            //		 as per the proper disposable pattern.
            Dispose(disposing: false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);

            // Finalize is an expensive operation - don't call twice
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True if the object's Dispose method has been explicitly called; false if the call
        /// has come through the object's finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}