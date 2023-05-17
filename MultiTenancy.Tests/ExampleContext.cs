using Microsoft.EntityFrameworkCore;
using MiltiTenancy.Tests.Entities;
using MultiTenancy;
using MultiTenancy.Core;
using MultiTenency.Core;
using MultiTenency.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenency.Tests
{
    public class ExampleContext : MultiTenancyContext
    {
        public ExampleContext(DbContextOptions options, IApplicationUser user, string schema) : base(options, user, schema)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddQueryFilterEntry<User>(e => e.Id < 1000);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
