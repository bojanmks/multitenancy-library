using MultiTenency.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency
{
    internal class LambdaParser<T>
    {
        public Expression<Func<T, bool>> ParseTenantIdLambda(string path, IApplicationUser user)
        {
            return DynamicExpressionParser.ParseLambda<T, bool>(new ParsingConfig(), true, $"{path}.TenantId = @0", user.TenantId);
        }
    }
}
