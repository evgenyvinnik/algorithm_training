// © Evgeny Vinnik

namespace Cache
{
    static class CacheUtils
    {
        public static bool IsPowerOfTwo(uint x)
        {
            return x != 0 && ModTwo(x, x - 1) == 0;
        }

        public static uint ModTwo(uint n, uint d)
        {
            return n & (d - 1);
        }
    }
}
