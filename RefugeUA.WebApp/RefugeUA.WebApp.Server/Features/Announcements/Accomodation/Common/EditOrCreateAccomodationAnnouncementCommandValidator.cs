using FluentValidation;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using SkiaSharp;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common
{
    public class EditOrCreateAccomodationAnnouncementCommandValidator : AbstractValidator<EditOrCreateAccomodationAnnouncementCommand>
    {
        public EditOrCreateAccomodationAnnouncementCommandValidator(IValidator<BaseEditOrCreateAnnouncementCommand> baseValidator)
        {
            Include(baseValidator);

            RuleFor(a => a.BuildingType)
                .NotEmpty().WithMessage("Тип будівлі є обов'язковим.")
                .MaximumLength(100).WithMessage("Тип будівлі не може перевищувати 100 символів.");

            RuleFor(a => (int)a.Floors)
                .GreaterThan(0).WithMessage("Кількість поверхів повинна бути додатним числом.")
                .LessThanOrEqualTo(60).WithMessage("Кількість поверхів не може перевищувати 60.");

            RuleFor(a => (int)a.NumberOfRooms)
                .GreaterThan(0).WithMessage("Кількість кімнат повинна бути додатним числом.");

            RuleFor(a => (int)a.Capacity)
                .GreaterThan(0).WithMessage("Вмістимість житла повинна бути додатним числом.");

            RuleFor(a => a.BuildingType)
                .NotEmpty().WithMessage("Тип будівлі є обов'язковим.")
                .MaximumLength(100).WithMessage("Тип будівлі не може перевищувати 100 символів.");

            RuleFor(a => a.AreaSqMeters)
                .GreaterThan(0).WithMessage("Площа повинна бути додатним числом.")
                .When(a => a.AreaSqMeters.HasValue);

            RuleFor(a => a.Price)
                .Must(price => price == null || price >= 0)
                .WithMessage("Ціна повинна бути додатним числом або залишатися порожньою.");

            RuleFor(a => a.Images)
                .Must(imgs => imgs.Count <= 6).WithMessage("Можна додати не більше 6 зображень до оголошення про житло.").When(command => command.Images != null);

            RuleForEach(a => a.Images)
                .Must(x => x!.Length / (1024 * 1024) <= 2).WithMessage("Розмір файлу зображення не може перевищувати 2 МБ.").When(x => x.Images != null)
                .Must(x => x!.FileName.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || x.FileName.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                .WithMessage("Файл повинен бути у форматі .jpg або .jpeg.").When(x => x.Images != null)
                .Must(x =>
                {
                    using MemoryStream ms = new MemoryStream();
                    x.CopyTo(ms);
                    SKBitmap bitmap = SKBitmap.Decode(ms.GetBuffer());

                    var aspectRatio = (float)bitmap.Width / bitmap.Height;

                    return aspectRatio >= 0.75f && aspectRatio <= 16f / 9;
                }).WithMessage("Співвідношення сторін зображення повинно бути в межах від 4:3 до 16:9.").When(x => x.Images != null)
                .Must(x =>
                {
                    using MemoryStream ms = new MemoryStream();
                    x.CopyTo(ms);
                    SKBitmap bitmap = SKBitmap.Decode(ms.GetBuffer());

                    return bitmap.Width >= 800 && bitmap.Height >= 600;
                }).WithMessage("Розмір зображення повинен бути щонайменше 800x600 пікселів.").When(x => x.Images != null);
        }
    }
}
