using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace StdApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class Entity
    {
        public int Id { get; set; }
    }

    public class Context
        :DbContext
    {
        public Context(DbContextOptions options)
            :base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=.;Initial Catalog=sample1;Integrated Security=true");
            //}
        }
    }

    public class DesingTimeContext
        : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=samples2;Integrated Security=true");

            return new Context(optionsBuilder.Options);
        }
    }
}
