using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common
{
    public static class MentalSupportArticleResultMapping
    {
        public static readonly Expression<Func<MentalSupportArticle, MentalSupportArticleResult>> Expression = a => new MentalSupportArticleResult()
        {
            Id = a.Id,
            AuthorId = a.AuthorId ?? 0,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            Author = new UserDtoWithId()
            {
                Id = a.Author.Id,
                CreatedAt = a.Author.CreatedAt,
                DateOfBirth = a.Author.DateOfBirth,
                Email = a.Author.Email,
                FirstName = a.Author.FirstName,
                LastName = a.Author.LastName,
                PhoneNumber = a.Author.PhoneNumber
            }
        };

        public static readonly Func<MentalSupportArticle, MentalSupportArticleResult> Func = a => new MentalSupportArticleResult()
        {
            Id = a.Id,
            AuthorId = a.AuthorId ?? 0,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            Author = new UserDtoWithId()
            {
                Id = a.Author.Id,
                CreatedAt = a.Author.CreatedAt,
                DateOfBirth = a.Author.DateOfBirth,
                Email = a.Author.Email,
                FirstName = a.Author.FirstName,
                LastName = a.Author.LastName,
                PhoneNumber = a.Author.PhoneNumber
            }
        };
    }
}
