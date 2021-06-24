using System;
using System.Linq;
using BiblioDomain.Models;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BiblioInfrastructure.Context
{
    public class BiblioStoreDbContext : DbContext
    {
        public BiblioStoreDbContext(DbContextOptions options) : base(options) {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BiblioStoreDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

    }
}
