using Journalist.Crm.Domain.Common;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public class PitchResultSet : ResultSetBase<Pitch>
    {
        public PitchResultSet(IReadOnlyCollection<Pitch> data, long totalItemCount, bool hasNextPage, bool hasPreviousPage) : base(data, totalItemCount, hasNextPage, hasPreviousPage)
        {
        }
    }
}
