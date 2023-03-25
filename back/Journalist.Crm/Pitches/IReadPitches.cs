using Journalist.Crm.Domain.Pitches.DataModels;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Pitches;

public interface IReadPitches
{
    Task<PitchDocument?> GetPitchAsync(string id, string userId, CancellationToken cancellationToken = default);
    Task<PitchResultSet> GetPitchesAsync(GetPitchesRequest request, CancellationToken cancellationToken = default);
}
