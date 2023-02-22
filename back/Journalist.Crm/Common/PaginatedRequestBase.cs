using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Common
{
    public abstract class PaginatedRequestBase : ContextFilteredRequestBase
    {
        protected PaginatedRequestBase(
            int skip,
            int take,
            string sortBy,
            string userId)
            : base(userId)
        {
            Skip = skip;
            Take = take;
            SortBy = sortBy;
        }

        public int Skip { get; }
        public int Take { get; }
        public string SortBy { get; }
    }
}
