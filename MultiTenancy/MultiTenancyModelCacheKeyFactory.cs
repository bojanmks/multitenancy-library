using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public class MultiTenancyModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime)
        {
            return new
            {
                Type = context.GetType(),
                Schema = context is MultiTenancyContext mtc ? mtc.Schema.ToUpperInvariant() : null,
                DesignTime = designTime
            };
        }

        public object Create(DbContext context) => Create(context, false);
    }
}
