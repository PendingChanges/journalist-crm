using Journalist.Crm.Domain.Clients.DataModels;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journalist.Crm.Neo4j.Clients
{
    public static class ClientMapper
    {
        public static Client ToClient(this INode node)
            => new Client(
                node.Properties[nameof(Client.Id)].As<string>(),
                node.Properties[nameof(Client.Name)].As<string>(),
                string.Empty
                );

        public static IReadOnlyCollection<Client> ToClients(this IEnumerable<IRecord> records)
            => records.Select(r => r["c"].As<INode>().ToClient()).ToList();
    }
}
