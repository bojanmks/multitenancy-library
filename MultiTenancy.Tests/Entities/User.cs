using MultiTenency.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency.Tests.Entities
{
    public class User : Entity, ITenantOwnedEntity
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }


        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
