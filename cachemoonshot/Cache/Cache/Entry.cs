// © Evgeny Vinnik
using System;

namespace Cache
{
    enum ValidityBit
    {
        Invalid,
        Valid
    }

    class Entry<TKey, TValue>
    {
        TValue cacheValue;

        public DateTime InsertTime { get; }

        public ValidityBit ValidityBit { get; private set; }

        public DateTime AccessTime { get; private set; }


        public TValue Value
        {
            get
            {
                AccessTime = DateTime.UtcNow;
                return cacheValue;
            }
            private set => cacheValue = value;
        }

        public Entry(TKey key, TValue value)
        {
            ValidityBit = ValidityBit.Valid;
            Value = value;
            InsertTime = DateTime.UtcNow;
            AccessTime = DateTime.UtcNow;
        }

        public void Invalidate()
        {
            ValidityBit = ValidityBit.Invalid;
        }
    }
}
