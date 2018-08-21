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
        volatile ValidityBit validityBit;

        public Entry(TKey key, TValue value)
        {
            validityBit = ValidityBit.Valid;
            Key = key;
            Value = value;
            AccessTime = DateTime.UtcNow;
        }

        public TKey Key { get; }

        public ValidityBit ValidityBit
        {
            get => validityBit;
        }

        public DateTime AccessTime { get; private set; }

        internal TValue Value
        {
            get
            {
                AccessTime = DateTime.UtcNow;
                return cacheValue;
            }
            private set => cacheValue = value;
        }

        public void Invalidate()
        {
            validityBit = ValidityBit.Invalid;
        }
    }
}
