using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Ideas.DataModels
{
    public class GetIdeasRequest : PaginatedRequestBase
    {
        private const int DEFAULT_PITCH_SKIP = 0;
        private const int DEFAULT_PITCH_TAKE = 10;
        private const string DEFAULT_PITCH_SORTBY = "Title";

        public GetIdeasRequest(
             string? pitchId,
            int? skip,
            int? take,
            string? sortBy,
            string userId) : base(
                skip ?? DEFAULT_PITCH_SKIP,
                take ?? DEFAULT_PITCH_TAKE,
                sortBy ?? DEFAULT_PITCH_SORTBY,
                userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; }
    }
}
