// © Evgeny Vinnik

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CacheUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static bool IsPowerOfTwo(uint x)
        {
            return x != 0 && ModTwo(x, x - 1) == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        internal static uint ModTwo(uint n, uint d)
        {
            return n & (d - 1);
        }
    }
}
