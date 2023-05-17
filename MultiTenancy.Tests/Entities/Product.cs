using MultiTenency.Entities;
using MultiTenency.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiltiTenancy.Tests.Entities
{
    public class Product : Entity, IMustHaveTenant
    {
        public string Name { get; set; }
        public decimal Price { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string TenantIdPath => "Category";
    }
}
