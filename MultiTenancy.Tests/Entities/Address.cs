using MultiTenancy.Entities;
using MultiTenency.Entities;
using MultiTenency.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiltiTenancy.Tests.Entities
{
    public class Address : Entity, IMustHaveUser, IMustHaveTenant
    {
        public string Value { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string TenantIdPath => "User";
    }
}
