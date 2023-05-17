using MultiTenency.Entities;
using MultiTenency.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiltiTenancy.Tests.Entities
{
    public class SubProduct : Entity, IMustHaveTenant
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string TenantIdPath => "Product.Category";
    }
}
