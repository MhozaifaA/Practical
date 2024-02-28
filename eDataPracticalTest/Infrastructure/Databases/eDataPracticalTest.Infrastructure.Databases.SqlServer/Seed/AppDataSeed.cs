using eDataPracticalTest.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.Infrastructure.Databases.SqlServer
{
    public static class AppDataSeed
    {
        /// <summary>
        /// Add default admin use .. 
        /// <para> UserName: admin, Password: admin</para>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static async Task<AppDbContext> AccountsSeedAsync(this AppDbContext context,
         IServiceProvider provider) //local scoped
        {

            var userManager = provider.GetRequiredService<UserManager<Account>>();

            var account = new Account()
            {
                Name = "eData Admin",
                UserName = "admin",
                Email = "admin@edata.ae",
            };

            Account? one = await userManager.FindByNameAsync(account.UserName);
            if(one is null)
              await userManager.CreateAsync(account, "admin");


            return context;
        }
    }
}
