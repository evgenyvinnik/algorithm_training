// © Evgeny Vinnik

namespace Cache
{
    public interface IMainStore<in TKey, out TValue>
    {
        TValue Get(TKey fin);
    }
}
