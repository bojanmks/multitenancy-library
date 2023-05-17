using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency.Tests.Entities
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}
