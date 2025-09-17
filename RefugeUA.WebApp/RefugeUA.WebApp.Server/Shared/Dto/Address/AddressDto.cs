namespace RefugeUA.WebApp.Server.Shared.Dto.Address
{
    public class AddressDto
    {
        public string Country { get; set; } = default!;

        public string Region { get; set; } = default!;

        public string District { get; set; } = default!;

        public string Settlement { get; set; } = default!;

        public string Street { get; set; } = default!;

        public string PostalCode { get; set; } = default!;
    }
}
