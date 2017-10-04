using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldsAndNavigation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public class Entity
        {
            private int _id;

            private List<Related> _relateds = new List<Related>();
            public IEnumerable<Related> Relateds => _relateds.ToList();
            //public int Id
            //{
            //    get
            //    {
            //        return _id;
            //    }
            //    set
            //    {
            //        _id = value;
            //    }
            //}
        }

        public class Related
        {
            public int Id { get; set; }
        }
        public class Context
            :DbContext
        {

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Entity>()
                    .Property("_id")
                    .IsRequired();

                modelBuilder.Entity<Entity>()
                    .Metadata.FindNavigation(nameof(Entity.Relateds))
                    .SetPropertyAccessMode(PropertyAccessMode.Field);

                modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            }
        }
    }


   
}
