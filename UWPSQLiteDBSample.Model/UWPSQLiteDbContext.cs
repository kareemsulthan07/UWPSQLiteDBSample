using Microsoft.EntityFrameworkCore;
using System;

namespace UWPSQLiteDBSample.Model
{
    public class UWPSQLiteDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public override string ToString() => Title;
    }
}
