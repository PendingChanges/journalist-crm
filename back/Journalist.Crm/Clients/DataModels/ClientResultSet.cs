using Journalist.Crm.Domain.Common;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Clients.DataModels;

public class ClientResultSet : ResultSetBase<ClientDocument>
{
    public ClientResultSet(
        IReadOnlyList<ClientDocument> data,
        long totalItemCount,
        bool hasNextPage,
        bool hasPreviousPage)
        : base(
            data,
            totalItemCount,
            hasNextPage,
            hasPreviousPage)
    {
    }
}
