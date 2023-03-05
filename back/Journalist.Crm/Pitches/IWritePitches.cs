using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Pitches
{
    public interface IWritePitches
    {
        Task<string> AddPitchAsync(PitchInput pitchInput, string userId, CancellationToken cancellationToken);
    }
}
