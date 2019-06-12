using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using lab4.Models;

namespace lab4.Data
{
    public class Context : DbContext
    {
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<ScheduleForWeek> SchedulesForWeek { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CitizensAppeal> CitizensAppeals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
