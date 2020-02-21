using Microsoft.EntityFrameworkCore;
using System;

namespace SQLiteSample.Model
{
    public class PeopleDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        private string dbFilePath;

        public PeopleDbContext()
        {
        }

        public PeopleDbContext(string dbFilePath)
        {
            this.dbFilePath = dbFilePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={dbFilePath}");
        }
    }
}
