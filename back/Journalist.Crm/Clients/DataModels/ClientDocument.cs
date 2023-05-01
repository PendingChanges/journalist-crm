using System.Collections.Generic;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.DataModels
{
    public record ClientDocument(EntityId Id, string Name, OwnerId OwnerId, List<string> PitchesIds);
}
