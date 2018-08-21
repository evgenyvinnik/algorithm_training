// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    public enum ValidityBit
    {
        /// <summary>
        /// 
        /// </summary>
        Invalid,

        /// <summary>
        /// 
        /// </summary>
        Valid
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Entry<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        TValue cacheValue;

        /// <summary>
        /// 
        /// </summary>
        volatile ValidityBit validityBit;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Entry(TKey key, TValue value)
        {
            validityBit = ValidityBit.Valid;
            Key = key;
            Value = value;
            AccessTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// 
        /// </summary>
        public ValidityBit ValidityBit
        {
            get => validityBit;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AccessTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal TValue Value
        {
            get
            {
                AccessTime = DateTime.UtcNow;
                return cacheValue;
            }

            private set => cacheValue = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Invalidate()
        {
            validityBit = ValidityBit.Invalid;
        }
    }
}
