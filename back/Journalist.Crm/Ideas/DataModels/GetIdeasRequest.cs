using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Ideas.DataModels
{
    public class GetIdeasRequest : PaginatedRequestBase
    {
        public GetIdeasRequest(
             string? pitchId,
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            string userId) : base(
                skip ?? Constants.DefaultPageNumber,
                take ?? Constants.DefaultPageSize,
                sortBy ?? Constants.DefaultIdeaSortBy,
                sortDirection ?? Constants.DefaultSortDirection,
                userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; }
    }
}
