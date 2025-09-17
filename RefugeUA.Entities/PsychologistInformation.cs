using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Interfaces;

namespace RefugeUA.Entities
{
    public class PsychologistInformation : Entity
    {
        public PsychologistInformation()
        {
            this.CreatedAt = DateTime.Now;
        }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public long? AuthorId { get; set; }

        public IUser Author { get; set; } = default!;

        public long ContactId { get; set; }

        public ContactInformation Contact { get; set; } = default!;
    }
}
