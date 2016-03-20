using GeoWcf.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWcf.Data
{
    public class GeoWcfDbContext : DbContext
    {
        public GeoWcfDbContext()
            : base("name=main")
        {
            Database.SetInitializer<GeoWcfDbContext>(null);
        }

        public DbSet<ZipCode> ZipCodeSet { get; set; }
        public DbSet<State> StateSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<ZipCode>().HasKey<int>(e => e.ZipCodeId).Ignore(e => e.EntityId)
                .HasRequired(e => e.State).WithMany().HasForeignKey(e => e.StateId);

            modelBuilder.Entity<State>().HasKey<int>(e => e.StateId).Ignore(e => e.EntityId);
        }
    }
}
