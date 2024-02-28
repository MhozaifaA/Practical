using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.Infrastructure.Databases.SqlServer
{
    //eDataPracticalTestDbContext ,  MrIdentityDbContext is only helper to get resolver for updated by and 
    public class AppDbContext : MrIdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //by call this I added indexing of DateCreating for performance on order by.
            // query filter on DateDeleted
            base.OnModelCreating(builder);

            //change the shcma and Name for Identity Tables -- Default came as dbo.AspNet...  which is is dont like it
            builder.Entity<Account>().ToTable("Users", "app");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles", "app");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "app");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "app");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "app");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "app");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "app");

        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
