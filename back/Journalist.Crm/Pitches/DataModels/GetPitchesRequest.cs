using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public class GetPitchesRequest : PaginatedRequestBase
    {
        private const int DEFAULT_PITCH_SKIP = 0;
        private const int DEFAULT_PITCH_TAKE = 10;
        private const string DEFAULT_PITCH_SORTBY = "Title";

        public GetPitchesRequest(
             string? clientId,
            int? skip,
            int? take,
            string? sortBy,
            string userId) : base(
                skip ?? DEFAULT_PITCH_SKIP,
                take ?? DEFAULT_PITCH_TAKE,
                sortBy ?? DEFAULT_PITCH_SORTBY,
                userId)
        {
            ClientId = clientId;
        }

        public string? ClientId { get; }
    }
}
