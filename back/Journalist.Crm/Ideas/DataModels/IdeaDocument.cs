using Journalist.Crm.Domain.Common;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Ideas.DataModels;

public record IdeaDocument(EntityId Id, string Name, string? Description, OwnerId OwnerId, List<string> PitchesIds);