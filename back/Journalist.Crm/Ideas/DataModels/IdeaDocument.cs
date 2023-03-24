using System.Collections.Generic;

namespace Journalist.Crm.Domain.Ideas.DataModels;

public record IdeaDocument(string Id, string Name, string? Description, string UserId, List<string> PitchesIds);