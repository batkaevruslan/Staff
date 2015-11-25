using System.Data.Entity;

using RB.Staff.Common.Pub.Entities;

using Staff.Data.EF.Migrations;
using Staff.Data.EF.Pvt;

namespace Staff.Data.EF.Pub
{
    public class StaffDbContext : DbContext
    {
        static StaffDbContext()
        {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<StaffDbContext, Configuration>() );
        }

        public StaffDbContext()
            : this( "StaffDataConnectionString" ) {}

        public StaffDbContext( string nameOrConnectionString )
            : base( nameOrConnectionString ) {}

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(
            DbModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );
            modelBuilder.Configurations.Add( new PersonMappings() );
        }
    }
}