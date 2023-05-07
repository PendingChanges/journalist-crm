using System.Collections.Generic;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.DataModels
{
    public record ClientDocument(string Id, string Name, string OwnerId, List<string> PitchesIds);
}
