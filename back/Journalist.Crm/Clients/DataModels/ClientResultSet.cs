using Journalist.Crm.Domain.Common;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Clients.DataModels;

public class ClientResultSet : ResultSetBase<Client>
{
    public ClientResultSet(
        IReadOnlyCollection<Client> data,
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
