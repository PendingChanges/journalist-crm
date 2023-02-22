using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Common
{
    public abstract class ResultSetBase<T>
    {
        protected ResultSetBase(
            IReadOnlyCollection<T> data,
            int totalItemCount,
            bool hasNextPage,
            bool hasPreviousPage)
        {
            Data = data;
            TotalItemCount = totalItemCount;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
        }

        public IReadOnlyCollection<T> Data { get; }
        public int TotalItemCount { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
    }
}
