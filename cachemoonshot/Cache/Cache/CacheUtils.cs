// © Evgeny Vinnik

namespace Cache
{
    /// <summary>
    /// Collection of utility functions used in cache implementation.
    /// </summary>
    internal static class CacheUtils
    {
        /// <summary>
        /// Function which checks whether a value is a power of two.
        /// </summary>
        /// <param name="x">Value to check.</param>
        /// <returns>True if value is a power of two, false otherwise.</returns>
        internal static bool IsPowerOfTwo(uint x)
        {
            return x != 0 && ModTwo(x, x - 1) == 0;
        }

        /// <summary>
        /// Simple trick to obtain mod by power of two.
        /// </summary>
        /// <param name="n">Value to perform mod operation on.</param>
        /// <param name="d">Power of two value.</param>
        /// <returns>Mod operation value.</returns>
        internal static uint ModTwo(uint n, uint d)
        {
            return n & (d - 1);
        }
    }
}
