using System.Collections.Generic;
using System.Linq;

namespace Optsol.Components.Infra.Data
{
    public class SearchResult<TEntity>
        where TEntity : class
    {
        public uint Page { get; private set; }

        public uint? PageSize { get; private set; }

        public long Total { get; private set; }

        public long TotalItems { get; private set; }

        public IEnumerable<TEntity> Items { get; private set; }

        public SearchResult(uint page, uint? pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public void SetTotalItems(IEnumerable<TEntity> items)
        {
            Items = items;
            TotalItems = items.Count();
        }

        public void SetTotalValue(long total)
        {
            Total = total;
        }
    }
}
