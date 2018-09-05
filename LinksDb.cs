using System;
using System.Collections.Generic;
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
            var connection = @"Server=localhost;Database=Links;User Id=sa;Password=My2Pass!";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
