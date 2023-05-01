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
                skip ?? Constants.DefaultPageNumber,
                take ?? Constants.DefaultPageSize,
                sortBy ?? Constants.DefaultPitchSortBy,
                sortDirection ?? Constants.DefaultSortDirection,
                userId)
        {
            ClientId = clientId;
            IdeaId = ideaId;
        }

        public string? ClientId { get; }

        public string? IdeaId { get; set; }
    }
}
