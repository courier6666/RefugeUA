using FluentValidation;
using Microsoft.AspNetCore.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;
using SkiaSharp;
using System.Drawing;

namespace RefugeUA.WebApp.Server.Features.Profile.EditMine
{
    public class EditMyProfileCommandValidator : AbstractValidator<EditMyProfileCommand>
    {
        public EditMyProfileCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ім'я є обов’язковим.")
                .MaximumLength(100).WithMessage("Ім'я не може бути довшим за 100 символів.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Прізвище є обов’язковим.")
                .MaximumLength(100).WithMessage("Прізвище не може бути довшим за 100 символів.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону є обов’язковим.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Номер телефону має бути у дійсному міжнародному форматі.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата народження є обов’язковою.")
                .LessThan(DateTime.Now).WithMessage("Дата народження має бути у минулому.")
                .LessThan(DateTime.Now.AddYears(-16)).WithMessage("Користувач має бути не молодший за 16 років.")
                .GreaterThan(new DateTime(1920, 1, 1)).WithMessage("Дата народження не може бути раніше 01.01.1920.");

            RuleFor(x => x.District)
                .MaximumLength(100).WithMessage("Назва району не може бути довшою за 100 символів.");

            RuleFor(x => x.ProfilePicture)
                .Must(x => x!.Length / (1024 * 1024) <= 2).WithMessage("Розмір файлу фотографії профілю не може перевищувати 2 МБ.").When(x => x != null)
                .Must(x => x!.FileName.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || x.FileName.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                    .WithMessage("Файл має бути у форматі .jpg або .jpeg.").When(x => x.ProfilePicture != null)
                .Must(x =>
                {
                    using MemoryStream ms = new MemoryStream();
                    x.CopyTo(ms);
                    SKBitmap bitmap = SKBitmap.Decode(ms.GetBuffer());

                    var aspectRatio = (float)bitmap.Width / bitmap.Height;

                    return aspectRatio >= 0.75f && aspectRatio <= 16f / 9;
                }).WithMessage("Співвідношення сторін зображення має бути між 3/4 та 16/9.").When(x => x.ProfilePicture != null)
                .Must(x =>
                {
                    using MemoryStream ms = new MemoryStream();
                    x.CopyTo(ms);
                    SKBitmap bitmap = SKBitmap.Decode(ms.GetBuffer());

                    return bitmap.Width >= 200 && bitmap.Height >= 200;
                }).WithMessage("Розмір зображення має бути не менше 200x200 пікселів.").When(x => x.ProfilePicture != null);
        }
    }
}
