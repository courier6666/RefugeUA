using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities.Abstracts
{
    public abstract class Entity : IEntity
    {
        public long Id { get; set; }
    }
}
