// © Evgeny Vinnik

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IMainStore<in TKey, out TValue>
    {
        TValue GetValue(TKey key);
    }
}
