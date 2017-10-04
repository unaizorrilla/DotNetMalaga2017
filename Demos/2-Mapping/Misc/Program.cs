using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Misc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working...");

            //BatchAndHilo();

            //FromSqlAndEFFunctions();

            //GlobalFilters();

            Warning_QueryClientEvaluation();
        }

        static void BatchAndHilo()
        {
            using (var context = new SampleContext())
            {
                var customers = Enumerable.Range(1, 50)
                    .Select(index => new Customer()
                    {
                        FirstName = $"FirstName{index}",
                        Tenant = $"Tenant{index % 5}"
                    });

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        static void FromSqlAndEFFunctions()
        {
            using (var context = new SampleContext())
            {

                var result = context.Customers
                    .FromSql("SELECT * FROM Customers where")
                    .Where(c => EF.Functions.Like(c.FirstName, "FirstName%"))
                    .ToList();
            }
        }

        static void GlobalFilters()
        {
            using (var context = new SampleContext())
            {
                //Uncomment global filter first
                var result = context.Customers
                    .ToList();
            }
        }

        static void Warning_QueryClientEvaluation()
        {
            using(var context = new SampleContext())
            {
                //Uncomment warning first
                var result = context.Customers
                    .Where(c=>OnClientFilter(c))
                    .ToList();
            }
        }

        static bool OnClientFilter(Customer c)
        {
            return c.FirstName.Split(',').Count() > 1;
        }
    }

    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Tenant { get; set; }
    }

    public class SampleContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=Misc;Integrated Security=true");

            optionsBuilder.ConfigureWarnings(builder =>
            {
                builder.Throw(CoreEventId.);
            });

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("hilo_seq_customers");






            modelBuilder.Entity<Customer>()
                .HasQueryFilter(c => c.Tenant == "Tenant1");
        }
    }
}
