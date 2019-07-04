using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Threading;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.MySql
{
    public static class MySqlDbHelper
    {
        /// <summary>
        /// Use MySql to connect to db
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="connectionString">Database connection string</param>
        public static void UseMySql(DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            Console.WriteLine($"Connection string: {connectionString}");

            TryConnectToDb(connectionString);

            try
            {
                optionsBuilder.UseMySql(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to db. \nReason: {ex.Message}");
            }
        }

        public static void TryConnectToDb(string connectionString, int maxRetries = 3)
        {
            var connection = new MySqlConnection(connectionString);
            var retries = 0;
            var isConnectionOpen = false;

            while (retries < maxRetries && !isConnectionOpen)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    isConnectionOpen = true;
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
