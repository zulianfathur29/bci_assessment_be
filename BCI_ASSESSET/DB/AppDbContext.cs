using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BCI_ASSESSET.Models;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BCI_ASSESSET.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().ToTable("users");
            model.Entity<User>().HasKey(u => u.Id);
            model.Entity<User>().HasIndex(u => u.Username).IsUnique();

            model.Entity<Project>()
                 .ToTable("project")
                 .Property(p => p.Id)
                 .HasDefaultValueSql("NULL")
                 .ValueGeneratedOnAdd()
                 .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            foreach (var entity in model.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToLower());
                }

                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.SetConstraintName(fk.GetConstraintName().ToLower());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToLower());
                }
            }
        }
        private string ToSnakeCase(string input)
        {
            return string.Concat(input.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
        }
    }
}
