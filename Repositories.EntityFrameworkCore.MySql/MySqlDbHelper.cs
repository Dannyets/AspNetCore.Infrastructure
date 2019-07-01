using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Threading;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.MySql
{
    public static class MySqlDbHelper
    {
        public static void MigrateDatabase<TDbContext>(IServiceProvider serivceProvider) where TDbContext : DbContext
        {
            using (var serviceScope = serivceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        /// <summary>
        /// Use MySql to connect to db
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="connectionString">Database connection string</param>
        public static void UseMySql(DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            Console.WriteLine($"Connection string: {connectionString}");

            TryConnectToDb(connectionString);

            optionsBuilder.UseMySql(connectionString);
        }

        public static void TryConnectToDb(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            var retries = 1;

            while (retries < 7)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    break;
                }
                catch (MySqlException)
                {
                    var waitTime = (int)Math.Pow(retries, 2) * 1000;
                    Thread.Sleep(waitTime);
                    retries++;
                }
            }
        }
    }
}
