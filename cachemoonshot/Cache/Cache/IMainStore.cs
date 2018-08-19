using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public interface IMainStore<in TKey, out TValue>
    {
        TValue Get(TKey fin);
    }
}
