using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.MySql
{
    public class BaseMySqlDbContext : DbContext
    {
        private IConfiguration _configuration;

        public BaseMySqlDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Reading connection string from configuration['DatabaseConnectionString']");

            var connectionString = _configuration.GetValue<string>("DatabaseConnectionString");

            MySqlDbHelper.UseMySql(optionsBuilder, connectionString);
        }
    }
}
