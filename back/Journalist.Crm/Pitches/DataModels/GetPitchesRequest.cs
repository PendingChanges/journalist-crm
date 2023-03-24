using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public class GetPitchesRequest : PaginatedRequestBase
    {
        private const int DEFAULT_PITCH_PAGENUMBER = 1;
        private const int DEFAULT_PITCH_PAGESIZE = 10;
        private const string DEFAULT_PITCH_SORTBY = "Title";

        public GetPitchesRequest(
             string? clientId,
             string? ideaId,
            int? skip,
            int? take,
            string? sortBy,
            string userId) : base(
                skip ?? DEFAULT_PITCH_PAGENUMBER,
                take ?? DEFAULT_PITCH_PAGESIZE,
                sortBy ?? DEFAULT_PITCH_SORTBY,
                userId)
        {
            ClientId = clientId;
            IdeaId = ideaId;
        }

        public string? ClientId { get; }

        public string? IdeaId { get; set; }
    }
}
