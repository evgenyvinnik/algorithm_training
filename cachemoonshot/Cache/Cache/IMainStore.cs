// © Evgeny Vinnik

namespace Cache
{
    /// <summary>
    /// Interface for the main store fallback.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    public interface IMainStore<in TKey, out TValue>
    {
        TValue GetValue(TKey key);
    }
}