using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class MentalSupportArticle : Entity
    {
        public MentalSupportArticle()
        {
            this.CreatedAt = DateTime.Now;
        }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public long? AuthorId { get; set; }

        public IUser Author { get; set; } = default!;
    }
}
