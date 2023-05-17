using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using MultiTenancy;
using MultiTenancy.Core;
using MultiTenancy.Entities;
using MultiTenancy.Extensions;
using MultiTenency;
using MultiTenency.Core;
using MultiTenency.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace MultiTenancy
{
    public abstract class MultiTenancyContext : DbContext
    {
        protected readonly IApplicationUser _user;
        public string Schema { get; }
        private List<IQueryFilterEntry> QueryFilterEntries { get; set; } = new List<IQueryFilterEntry>();

        public MultiTenancyContext(DbContextOptions options, IApplicationUser user, string schema) : base(options)
        {
            _user = user;
            Schema = schema;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IModelCacheKeyFactory, MultiTenancyModelCacheKeyFactory>();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                       .ToList()
                       .ForEach(entityType =>
                       {
                           modelBuilder.AddITenantOwnedEntityQueryFilter(entityType.ClrType, _user);
                           modelBuilder.AddIMustHaveTenantQueryFilter(entityType.ClrType, _user);
                           modelBuilder.AddIMustHaveUserQueryFilter(entityType.ClrType, _user);
                           modelBuilder.AddCustomQueryFilters(entityType.ClrType, QueryFilterEntries);
                       });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is ITenantOwnedEntity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if(!(_user is IApplicationSuperUserGlobal))
                            {
                                e.TenantId = _user.TenantId;
                            }
                            break;

                        case EntityState.Modified:
                            if (!(_user is IApplicationSuperUserGlobal))
                            {
                                e.TenantId = _user.TenantId;
                            }
                            break;
                    }
                }

                if (entry.Entity is IMustHaveUser eUser)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            eUser.UserId = _user.UserId;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        protected void AddQueryFilterEntry<T>(Expression<Func<T, bool>> expression)
        {
            QueryFilterEntries.Add(new QueryFilterEntry<T>
            {
                Expression = expression
            });
        }
    }
}