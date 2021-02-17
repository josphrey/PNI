using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PNI.Entities;

namespace PNI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Buyers.Any())
            {
                return;
            }

            var vBuyers = new Buyers[]
            {
                new Buyers{Name = "ABCCorporationTestUnit", Address = "TestAddress"},
            };

            foreach (Buyers b in vBuyers)
            {
                context.Buyers.Add(b);
            }

            context.SaveChanges();
        }
    }
}
