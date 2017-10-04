using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Testing
{
    public class UnitTest1
        :IClassFixture<EFFixture>
    {
        EFFixture _fixture;

        public UnitTest1(EFFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void Test1()
        {
            using (var context = _fixture.Context)
            {
                context.Entities.ToListAsync();
            }

        }
    }

    public class EFFixture 
    {
        public SampleContext Context { get; private set; }

        public EFFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
            optionsBuilder.UseInMemoryDatabase("memoried_database");

            Context = new SampleContext(optionsBuilder.Options);
        }
    }

    public class SampleContext
        :DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public SampleContext(DbContextOptions options) : base(options) { }


    }

    public class Entity
    {
        public int Id { get; set; }

    }
}
