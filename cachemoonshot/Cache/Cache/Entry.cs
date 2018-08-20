// © Evgeny Vinnik

using System;

namespace Cache
{
    public enum ValidityBit
    {
        Invalid,
        Valid
    }

    public class Entry<TKey, TValue>
    {
        TValue cacheValue;

        public TKey Key { get; }

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
            Key = key;
            Value = value;
            AccessTime = DateTime.UtcNow;
        }

        public void Invalidate()
        {
            ValidityBit = ValidityBit.Invalid;
        }
    }
}
