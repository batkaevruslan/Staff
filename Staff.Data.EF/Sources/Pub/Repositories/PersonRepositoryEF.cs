using System;
using System.Linq;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;

namespace Staff.Data.EF.Pub.Repositories
{
    public class PersonRepositoryEF : IPersonRepository
    {
        private readonly StaffDbContext _dbContext;

        public PersonRepositoryEF( StaffDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IQueryable<Person> All()
        {
            return _dbContext.Persons.AsQueryable();
        }

        public int Insert(
            Person createData )
        {
            _dbContext.Persons.Add( createData );
            _dbContext.SaveChanges();
            return createData.Id;
        }

        public Person GetById(
            int id )
        {
            return _dbContext.Persons.Find( id );
        }

        public void Update(
            Person person )
        {
            if( person == null ) {
                throw new ArgumentNullException( "person" );
            }
            _dbContext.SaveChanges();
        }
    }
}