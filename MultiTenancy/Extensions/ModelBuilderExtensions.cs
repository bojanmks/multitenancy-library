using Microsoft.EntityFrameworkCore;
using MultiTenancy.Core;
using MultiTenancy.Entities;
using MultiTenency;
using MultiTenency.Core;
using MultiTenency.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void AddITenantOwnedEntityQueryFilter(this ModelBuilder builder, Type type, IApplicationUser user)
        {
            if (typeof(ITenantOwnedEntity).IsAssignableFrom(type))
            {
                EntityTypeBuilderExtensions.AddQueryFilter<ITenantOwnedEntity>(builder.Entity(type),
                                                                               e => e.TenantId == user.TenantId || user is IApplicationSuperUserGlobal);
            }
        }

        internal static void AddIMustHaveTenantQueryFilter(this ModelBuilder builder, Type type, IApplicationUser user)
        {
            if (typeof(IMustHaveTenant).IsAssignableFrom(type) && !(user is IApplicationSuperUserGlobal))
            {
                var instance = Activator.CreateInstance(type);
                var tenantIdPath = (instance as IMustHaveTenant).TenantIdPath;

                var lambdaParserGenericType = typeof(LambdaParser<>).MakeGenericType(type);
                dynamic lambdaParserInstance = Activator.CreateInstance(lambdaParserGenericType);

                var expression = lambdaParserInstance.ParseTenantIdLambda(tenantIdPath, user);

                EntityTypeBuilderExtensions.AddQueryFilter(builder.Entity(type), expression);
            }
        }

        internal static void AddIMustHaveUserQueryFilter(this ModelBuilder builder, Type type, IApplicationUser user)
        {
            if (typeof(IMustHaveUser).IsAssignableFrom(type))
            {
                EntityTypeBuilderExtensions.AddQueryFilter<IMustHaveUser>(builder.Entity(type),
                                     e => (user is IApplicationSuperUserWithinTenant) || e.UserId == user.UserId);
            }
        }

        internal static void AddCustomQueryFilters(this ModelBuilder builder, Type type, List<IQueryFilterEntry> entries)
        {
            foreach (var item in entries ?? Enumerable.Empty<IQueryFilterEntry>())
            {
                if (item.Type.IsAssignableFrom(type))
                {
                    Type entryGenericType = typeof(QueryFilterEntry<>).MakeGenericType(item.Type);
                    dynamic entry = Convert.ChangeType(item, entryGenericType);

                    EntityTypeBuilderExtensions.AddQueryFilter(builder.Entity(type), entry.Expression);
                }
            }
        }
    }
}
