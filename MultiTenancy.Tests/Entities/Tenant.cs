using MultiTenency.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency.Tests.Entities
{
    public class Tenant : Entity
    {
        public string Name { get; set; }
    }
}
