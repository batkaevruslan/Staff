using System.Data.Entity;

using RB.Staff.Common.Pub.Entities;

using Staff.Data.EF.Pvt;

namespace Staff.Data.EF
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext() {}

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