using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class Address : Entity
    {
        public string Country { get; set; } = default!;

        public string Region { get; set; } = default!;

        public string District { get; set; } = default!;

        public string Settlement { get; set; } = default!;

        public string Street { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public Announcement? Announcement { get; set; } = default!;

        public VolunteerEvent? VolunteerEvent { get; set; } = default!;
    }
}
