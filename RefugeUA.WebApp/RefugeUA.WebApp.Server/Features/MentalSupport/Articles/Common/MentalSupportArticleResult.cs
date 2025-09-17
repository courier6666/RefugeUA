using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Shared.Dto.User;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common
{
    public class MentalSupportArticleResult
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public long AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserDtoWithId Author { get; set; } = default!;
    }
}
