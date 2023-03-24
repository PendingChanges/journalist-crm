using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.DataModels
{
    public class GetClientsRequest : PaginatedRequestBase
    {
        private const int DEFAULT_CLIENT_PAGENUMBER = 1;
        private const int DEFAULT_CLIENT_PAGESIZE = 10;
        private const string DEFAULT_CLIENT_SORTBY = "Name";
        public GetClientsRequest(
            string? pitchId,
            int? skip,
            int? take,
            string? sortBy,
            string userId)
            : base(
            skip ?? DEFAULT_CLIENT_PAGENUMBER,
            take ?? DEFAULT_CLIENT_PAGESIZE,
            sortBy ?? DEFAULT_CLIENT_SORTBY,
            userId)
        {
            PitchId = pitchId;
        }

        public string? PitchId { get; set; }
    }
}
