using MultiTenency.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiltiTenancy.Tests.Core
{
    public class ApplicationUser : IApplicationUser
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }

    public class ApplicationSuperUserWithinTenant : ApplicationUser, IApplicationSuperUserWithinTenant
    {

    }

    public class ApplicationSuperUserGlobal : ApplicationUser, IApplicationSuperUserGlobal
    {

    }
}
