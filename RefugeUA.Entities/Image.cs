using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class Image : Entity
    {
        public string Path { get; set; } = default!;
    }
}
