using System.Linq;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;

namespace Staff.Data.EF.Repositories
{
    public class PersonRepositoryEF: IPersonRepository
    {
        private readonly StaffDbContext _dbContext;
        public PersonRepositoryEF(StaffDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Person> All()
        {
            return _dbContext.Persons.AsQueryable();
        }
    }
}