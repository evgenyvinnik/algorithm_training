// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// Enum used to report the source of cache invalidation.
    /// </summary>
    public enum InvalidationSource
    {
        /// <summary>
        /// Cache entry was explicitly invalidated on user's request.
        /// </summary>
        User,

        /// <summary>
        /// Cache entry was invalidated as a result of cache eviction algorithm.
        /// </summary>
        Eviction,

        /// <summary>
        /// Cache entry was invalidated as new value for the previously stored key was brought in.
        /// </summary>
        Replacement
    }

    /// <summary>
    /// Class for invalidation notification handler event arguments.
    /// </summary>
    public class InvalidationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets reported <see cref="InvalidationSource"/>.
        /// </summary>
        public InvalidationSource Source { get; set; }
    }
}
