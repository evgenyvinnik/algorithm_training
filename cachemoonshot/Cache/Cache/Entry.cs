﻿// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
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

        internal event EventHandler<InvalidationEventArgs> InvalidationListener;

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
        /// <param name="source"></param>
        public void Invalidate(InvalidationSource source)
        {
            validityBit = ValidityBit.Invalid;
            InvalidationListener?.Invoke(
                this,
                new InvalidationEventArgs
                {
                    Source = source
                });
        }
    }
}
