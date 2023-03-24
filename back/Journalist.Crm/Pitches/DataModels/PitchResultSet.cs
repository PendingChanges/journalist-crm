using Journalist.Crm.Domain.Common;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public class PitchResultSet : ResultSetBase<PitchDocument>
    {
        public PitchResultSet(IReadOnlyList<PitchDocument> data, long totalItemCount, bool hasNextPage, bool hasPreviousPage) : base(data, totalItemCount, hasNextPage, hasPreviousPage)
        {
        }
    }
}
