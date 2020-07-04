using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using Shipping.Model;

namespace Shipping.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Party> Parties { get; set; }
        public DbSet<GeneralParameter> GeneralParameters { get; set; }
        public DbSet<PartyPerBox> PartPerBoxes { get; set; }
        
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
