// © Evgeny Vinnik

namespace Cache
{
    internal static class CacheUtils
    {
        internal static bool IsPowerOfTwo(uint x)
        {
            return x != 0 && ModTwo(x, x - 1) == 0;
        }

        internal static uint ModTwo(uint n, uint d)
        {
            return n & (d - 1);
        }
    }
}
