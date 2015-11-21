using System.Linq;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;
using RB.Staff.Common.Pub.Types;

namespace Staff.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService( IPersonRepository _personRepository )
        {
            this._personRepository = _personRepository;
        }

        public PagedSearchResult<Person> SearchPersons(
            PersonSearchParameters searchParameters,
            int pageNumber,
            int pageSize )
        {
            var query = _personRepository.All();
            if( searchParameters.IsActive.HasValue ) {
                query = query.Where( p => p.IsActive == searchParameters.IsActive.Value );
            }
            query = query.OrderBy( p => p.Name );
            return new PagedSearchResult<Person>( query, pageNumber, pageSize );
        }
    }
}