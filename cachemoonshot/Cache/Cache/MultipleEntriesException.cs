// © Evgeny Vinnik

using System;

namespace Cache
{
    internal class MultipleEntriesException : Exception
    {
        public MultipleEntriesException()
        {
        }

        public MultipleEntriesException(string message) : base(message)
        {
        }

        public MultipleEntriesException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
