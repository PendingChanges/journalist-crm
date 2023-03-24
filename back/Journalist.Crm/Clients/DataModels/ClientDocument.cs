using System.Collections.Generic;

namespace Journalist.Crm.Domain.Clients.DataModels
{
    public record ClientDocument(string Id, string Name, string UserId, List<string> PitchesIds);
}
