using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CheckLinksConsole2
{
    public class LinksDb : DbContext
    {
        public DbSet<LinkCheckResult> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // MSSQL:
            //var connection = @"Server=localhost;Database=Links;User Id=sa;Password=My2Pass!";
            //optionsBuilder.UseSqlServer(connection);

            // MySQL (Official):
            //var connection = "server=localhost;userid=root;pwd=password;database=Links;sslmode=none;";
            //optionsBuilder.UseMySQL(connection);  //error
            // MySQL (p):
            //var connection = "server=localhost;userid=root;pwd=password;database=Links;sslmode=none;";
            //optionsBuilder.UseMySql(connection);  //error

            // SQLite:
            var databaseLocation = Path.Combine(Directory.GetCurrentDirectory(), "links.db");
            optionsBuilder.UseSqlite($"Filename={databaseLocation}");
        }
    }
}
