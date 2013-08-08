namespace Slade
{
    /// <summary>
    /// Provides access to a shared array of a fixed-size of zero that is typed to a specified type.
    /// </summary>
    /// <typeparam name="T">The type of array to get an empty instance of.</typeparam>
    public static class EmptyArray<T>
    {
        /// <summary>
        /// The shared empty instance of the specified strongly-typed array.
        /// </summary>
        public static readonly T[] Instance = new T[0];
    }
}