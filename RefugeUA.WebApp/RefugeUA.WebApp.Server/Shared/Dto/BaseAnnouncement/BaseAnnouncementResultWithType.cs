namespace RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement
{
    public class BaseAnnouncementResultWithType : BaseAnnouncementResult
    {
        public AnnouncementType Type { get; set; } = default!;
    }
}
