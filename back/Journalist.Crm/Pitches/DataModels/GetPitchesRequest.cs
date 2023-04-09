using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public class GetPitchesRequest : PaginatedRequestBase
    {
        public GetPitchesRequest(
             string? clientId,
             string? ideaId,
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            string userId) : base(
                skip ?? Constants.DEFAULT_PAGENUMBER,
                take ?? Constants.DEFAULT_PAGESIZE,
                sortBy ?? Constants.DEFAULT_PITCH_SORTBY,
                sortDirection ?? Constants.DEFAULT_SORT_DIRECTION,
                userId)
        {
            ClientId = clientId;
            IdeaId = ideaId;
        }

        public string? ClientId { get; }

        public string? IdeaId { get; set; }
    }
}
