// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// Exception that is getting thrown if cache miss is happening.
    /// </summary>
    public class CacheMissException : Exception
    {
        public CacheMissException()
        {
        }

        public CacheMissException(string message) : base(message)
        {
        }

        public CacheMissException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
