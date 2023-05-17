using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy.Core
{
    public class QueryFilterEntry<T> : IQueryFilterEntry
    {
        public Type Type => typeof(T);
        public Expression<Func<T,bool>> Expression { get; set; }
    }

    public interface IQueryFilterEntry
    {
        public Type Type { get; }

    }
}
