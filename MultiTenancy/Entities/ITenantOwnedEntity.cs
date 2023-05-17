using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency.Entities
{
    public interface ITenantOwnedEntity
    {
        public int TenantId { get; set; }
    }
}
