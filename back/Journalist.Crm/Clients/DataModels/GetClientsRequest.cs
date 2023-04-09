using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.DataModels
{
    public class GetClientsRequest : PaginatedRequestBase
    {
        public GetClientsRequest(
            string? pitchId,
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            string userId)
            : base(
            skip ?? Constants.DEFAULT_PAGENUMBER,
            take ?? Constants.DEFAULT_PAGESIZE,
            sortBy ?? Constants.DEFAULT_CLIENT_SORTBY,
            sortDirection ?? Constants.DEFAULT_SORT_DIRECTION,
            userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; set; }
    }
}
