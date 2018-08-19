using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    class MultipleEntriesException : Exception
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
