using System.Linq;

using PagedList;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;

namespace Staff.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService( IPersonRepository _personRepository )
        {
            this._personRepository = _personRepository;
        }

        public IPagedList<Person> SearchPersons(
            bool? isActive,
            int pageNumber,
            int pageSize )
        {
            var query = _personRepository.All();
            if( isActive.HasValue ) {
                query = query.Where( p => p.IsActive == isActive.Value );
            }
            query = query.OrderBy( p => p.Name );
            return query.ToPagedList( pageNumber, pageSize );
        }

        public int Create(
            Person createData )
        {
            return _personRepository.Insert( createData );
        }

        public Person GetById(
            int id )
        {
            return _personRepository.GetById( id );
        }

        public void Update(
            Person person )
        {
            _personRepository.Update( person );
        }
    }
}