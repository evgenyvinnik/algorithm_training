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
    public enum InvalidationSource
    {
        /// <summary>
        /// 
        /// </summary>
        User,

        /// <summary>
        /// 
        /// </summary>
        Eviction,

        /// <summary>
        /// 
        /// </summary>
        Replacement
    }

    /// <summary>
    /// 
    /// </summary>
    public class InvalidationEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public InvalidationSource Source { get; set; }
    }
}
