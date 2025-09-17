namespace RefugeUA.WebApp.Server.Shared.Dto.PagingInfo
{
    public interface IPagingInfoQuery
    {
        public int Page { get; set; }

        public int PageLength { get; set; }
    }
}
