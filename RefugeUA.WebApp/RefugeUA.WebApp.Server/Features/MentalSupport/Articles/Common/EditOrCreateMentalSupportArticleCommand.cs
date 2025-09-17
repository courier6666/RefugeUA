namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common
{
    public class EditOrCreateMentalSupportArticleCommand
    {
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;
    }
}
