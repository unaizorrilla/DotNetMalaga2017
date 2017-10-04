using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
namespace BasicMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public interface IAuditable { }

    public class MyEntity
        :IAuditable
    {
        public int Id { get; set; }
    }

    public class Context
        :DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyEntity>()
                .Property<string>("UpdatedBy")
                .IsRequired();

            modelBuilder.Entity<MyEntity>()
                .Property<DateTime>("UpdatedOn")
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=dd;Integrated Security=true");
        }

        public override int SaveChanges()
        {
            var entries = this.ChangeTracker.Entries();

            foreach (var item in entries)
            {
                if (typeof(IAuditable).IsAssignableFrom(item.Entity.GetType()))
                {
                    item.Property("UpdatedBy").CurrentValue = "Unai";
                    item.Property("UpdatedOn").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
