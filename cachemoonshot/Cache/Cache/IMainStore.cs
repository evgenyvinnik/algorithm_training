// © Evgeny Vinnik

namespace Cache
{
    public interface IMainStore<in TKey, out TValue>
    {
        TValue GetValue(TKey key);
    }
}
