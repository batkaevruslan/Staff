using System.Data.Entity.ModelConfiguration;

using Staff.Common;

namespace Staff.Data.EF.Pvt
{
    internal class PersonMappings : EntityTypeConfiguration<Person>
    {
        public PersonMappings()
        {
            ToTable( "Person" );
            HasKey( p => p.Id );
            Property( p => p.Name ).IsRequired().HasMaxLength( 100 );
            Property( p => p.Position ).IsRequired().HasMaxLength( 100 );
            Property( p => p.IsActive ).IsRequired();
            Property( p => p.Salary ).HasPrecision( 8, 2 );
        }
    }
}