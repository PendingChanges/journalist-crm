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
                skip ?? Constants.DEFAULT_PAGENUMBER,
                take ?? Constants.DEFAULT_PAGESIZE,
                sortBy ?? Constants.DEFAULT_IDEA_SORTBY,
                sortDirection ?? Constants.DEFAULT_SORT_DIRECTION,
                userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; }
    }
}
