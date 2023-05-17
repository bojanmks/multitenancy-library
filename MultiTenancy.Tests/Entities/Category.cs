using MultiTenency.Entities;
using MultiTenency.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiltiTenancy.Tests.Entities
{
    public class Category : Entity, ITenantOwnedEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
