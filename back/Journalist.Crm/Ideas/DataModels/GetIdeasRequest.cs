using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Ideas.DataModels
{
    public class GetIdeasRequest : PaginatedRequestBase
    {
        private const int DEFAULT_IDEA_PAGENUMBER = 1;
        private const int DEFAULT_IDEA_PAGE_SIZE = 10;
        private const string DEFAULT_IDEA_SORTBY = "Title";

        public GetIdeasRequest(
             string? pitchId,
            int? skip,
            int? take,
            string? sortBy,
            string userId) : base(
                skip ?? DEFAULT_IDEA_PAGENUMBER,
                take ?? DEFAULT_IDEA_PAGE_SIZE,
                sortBy ?? DEFAULT_IDEA_SORTBY,
                userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; }
    }
}
