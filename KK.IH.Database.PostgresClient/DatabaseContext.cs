﻿namespace KK.IH.Tools.Database.PostgresClient
{
    using KK.IH.Database.PostgresClient.Models;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}