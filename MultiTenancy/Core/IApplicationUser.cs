using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency.Core
{
    public interface IApplicationUser
    {
        public int UserId { get; }
        public int TenantId { get; }
    }

    public interface IApplicationSuperUserWithinTenant : IApplicationUser
    {

    }

    public interface IApplicationSuperUserGlobal : IApplicationSuperUserWithinTenant
    {

    }
}
