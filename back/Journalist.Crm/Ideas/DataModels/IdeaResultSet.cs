using Journalist.Crm.Domain.Common;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Ideas.DataModels
{
    public class IdeaResultSet : ResultSetBase<IdeaDocument>
    {
        public IdeaResultSet(IReadOnlyList<IdeaDocument> data, long totalItemCount, bool hasNextPage, bool hasPreviousPage) : base(data, totalItemCount, hasNextPage, hasPreviousPage)
        {
        }
    }
}
