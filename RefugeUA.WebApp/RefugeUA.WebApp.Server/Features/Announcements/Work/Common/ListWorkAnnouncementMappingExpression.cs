using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using RefugeUA.WebApp.Server.Shared.Dto.WorkCategory;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Common
{
    public static class ListWorkAnnouncementMappingExpression
    {
        public static Expression<Func<WorkAnnouncement, WorkAnnouncementResult>> ListWorkAnnouncementToDtoExpression => a => new WorkAnnouncementResult()
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            JobPosition = a.JobPosition,
            CompanyName = a.CompanyName,
            SalaryLower = a.SalaryLower,
            SalaryUpper = a.SalaryUpper,
            RequirementsContent = a.RequirementsContent,
            WorkCategoryId = a.WorkCategoryId,
            WorkCategory = new WorkCategoryDtoWithId()
            {
                Id = a.WorkCategory.Id,
                Name = a.WorkCategory.Name,
            },
            Author = new UserDtoWithId()
            {
                Id = a.AuthorId ?? 0,
                FirstName = a.Author.FirstName,
                LastName = a.Author.LastName,
                CreatedAt = a.Author.CreatedAt,
                DateOfBirth = a.Author.DateOfBirth,
                Email = a.Author.Email,
                PhoneNumber = a.Author.PhoneNumber,
            },
            Address = new AddressDtoWithId()
            {
                Id = a.AddressId,
                Country = a.Address.Country,
                Region = a.Address.Region,
                District = a.Address.District,
                Settlement = a.Address.Settlement,
                Street = a.Address.Street,
                PostalCode = a.Address.PostalCode,
            },
            ResponsesCount = a.Responses.Count,
            AnnouncementGroups = a.Groups
                .Select(g => new AnnouncementGroupDtoWithId()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToList(),
            IsClosed = a.IsClosed
        };
    }
}
