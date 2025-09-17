namespace RefugeUA.WebApp.Server.Shared.Dto.User
{
    public class UserDtoWithIdAdmin : UserDtoWithId
    {
        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        public bool IsAccepted { get; set; }

        public string? District { get; set; }
    }
}
