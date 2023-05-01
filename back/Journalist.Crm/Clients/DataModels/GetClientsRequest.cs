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
            skip ?? Constants.DefaultPageNumber,
            take ?? Constants.DefaultPageSize,
            sortBy ?? Constants.DefaultClientSortBy,
            sortDirection ?? Constants.DefaultSortDirection,
            userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; set; }
    }
}
