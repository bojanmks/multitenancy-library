using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MiltiTenancy.Tests.Core;
using MiltiTenancy.Tests.Entities;
using MultiTenancy;
using MultiTenency.Entities;
using MultiTenency.Tests;
using MultiTenency.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiltiTenancy.Tests
{
    public class MultiTenancyFixture
    {
        public ExampleContext Context1 { get; }
        public ExampleContext Context2 { get; }
        public ExampleContext Context3 { get; }
        public ExampleContext Context4 { get; }
        public ExampleContext Context5 { get; }

        public MultiTenancyFixture()
        {
            #region AppUsers

            var user1 = new ApplicationUser
            {
                UserId = 1,
                TenantId = 1
            };

            var user2 = new ApplicationUser
            {
                UserId = 2,
                TenantId = 2
            };

            var user3 = new ApplicationSuperUserWithinTenant
            {
                UserId = 3,
                TenantId = 2
            };

            var user4 = new ApplicationUser
            {
                UserId = 4,
                TenantId = 2
            };

            var user5 = new ApplicationSuperUserGlobal
            {
                UserId = 5,
                TenantId = 1
            };

            #endregion


            #region SqlServer

            //var builder = new DbContextOptionsBuilder();
            //builder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=Diplomski;Integrated Security=True").UseLazyLoadingProxies();

            //Context1 = new ExampleContext(builder.Options, user1, "User1");
            //Context2 = new ExampleContext(builder.Options, user2, "User2");
            //Context3 = new ExampleContext(builder.Options, user3, "User3");
            //Context4 = new ExampleContext(builder.Options, user4, "User4");

            #endregion


            #region Sqlite

            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite(@"Data Source=Shareable;Mode=Memory;Cache=Shared").UseLazyLoadingProxies();

            Context1 = new ExampleContext(builder.Options, user1, "User1");
            Context1.Database.OpenConnection();
            Context1.Database.EnsureCreated();

            Context2 = new ExampleContext(builder.Options, user2, "User2");
            Context2.Database.OpenConnection();
            Context2.Database.EnsureCreated();

            Context3 = new ExampleContext(builder.Options, user3, "User3");
            Context3.Database.OpenConnection();
            Context3.Database.EnsureCreated();

            Context4 = new ExampleContext(builder.Options, user4, "User4");
            Context4.Database.OpenConnection();
            Context4.Database.EnsureCreated();

            Context5 = new ExampleContext(builder.Options, user5, "User5");
            Context5.Database.OpenConnection();
            Context5.Database.EnsureCreated();

            #endregion


            #region Tenants

            Context1.Tenants.Add(new Tenant
            {
                Id = 1,
                Name = "Tenant 1"
            });

            Context1.Tenants.Add(new Tenant
            {
                Id = 2,
                Name = "Tenant 2"
            });

            #endregion


            #region DBUsers

            Context1.Users.Add(new User
            {
                Id = 1,
                Email = "u1@gmail.com",
                FullName = "User 1",
                Username = "user1"
            });

            Context2.Users.Add(new User
            {
                Id = 2,
                Email = "u2@gmail.com",
                FullName = "User 2",
                Username = "user2"
            });

            Context3.Users.Add(new User
            {
                Id = 3,
                Email = "u3@gmail.com",
                FullName = "Super User 3",
                Username = "user3"
            });

            Context4.Users.Add(new User
            {
                Id = 4,
                Email = "u4@gmail.com",
                FullName = "User 4",
                Username = "user4"
            });

            Context5.Users.Add(new User
            {
                Id = 5,
                Email = "u5@gmail.com",
                FullName = "User 5",
                Username = "user5"
            });

            Context1.Users.Add(new User
            {
                Id = 1000,
                Email = "u1000@gmail.com",
                FullName = "User 1000",
                Username = "user11000"
            });

            #endregion


            #region Addresses

            Context1.Addresses.Add(new Address
            {
                Value = "Adresa K1, Beograd"
            });

            Context2.Addresses.Add(new Address
            {
                Value = "Adresa K2, Novi Sad"
            });

            Context3.Addresses.Add(new Address
            {
                Value = "Adresa K3, Beograd"
            });

            Context4.Addresses.Add(new Address
            {
                Value = "Adresa K4, Beograd"
            });

            #endregion


            #region Categories

            var cat1 = Context1.Categories.Add(new Category
            {
                Name = "Kategorija 1"
            });

            #endregion


            #region Products

            var product1 = Context1.Products.Add(new Product
            {
                Name = "Product 1",
                Price = 2.25m,
                Category = cat1.Entity
            });

            var product2 = Context1.Products.Add(new Product
            {
                Name = "Product 2",
                Price = 6.25m,
                Category = cat1.Entity
            });

            #endregion


            #region Subproducts

            Context1.SubProducts.Add(new SubProduct
            {
                Name = "SubProduct 1",
                Product = product1.Entity
            });

            #endregion

            Context1.SaveChanges();
            Context2.SaveChanges();
            Context3.SaveChanges();
            Context4.SaveChanges();
            Context5.SaveChanges();
        }
    }
}
