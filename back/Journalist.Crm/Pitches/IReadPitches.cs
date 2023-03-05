using Journalist.Crm.Domain.Pitches.DataModels;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Pitches;

public interface IReadPitches
{
    Task<PitchResultSet> GetPitchesAsync(GetPitchesRequest request, CancellationToken cancellationToken = default);
    Task<long> GetPitchesNbByClientIdAsync(string clientId, string userId, CancellationToken cancellationToken = default);
    Task<long> GetPitchesNbByIdeaIdAsync(string id, string userId, CancellationToken cancellationToken);
}
