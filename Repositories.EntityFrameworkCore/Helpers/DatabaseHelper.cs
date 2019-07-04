using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Helpers
{
    public static class DatabaseHelper
    {
        public static bool MigrateDatabase<TDbContext>(IServiceProvider serivceProvider) where TDbContext : DbContext
        {
            try
            {
                using (var serviceScope = serivceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<TDbContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to migrate database. \nError Message: {ex.Message}");

                return false;
            }

            return true;
        }

        public static bool EnsureDatabaseCreated<TDbContext>(IServiceProvider serivceProvider) where TDbContext : DbContext
        {
            try
            {
                using (var serviceScope = serivceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<TDbContext>())
                    {
                        context.Database.EnsureCreated();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to ensure database is created. \nError Message: {ex.Message}");

                return false;
            }

            return true;
        }
    }
}
