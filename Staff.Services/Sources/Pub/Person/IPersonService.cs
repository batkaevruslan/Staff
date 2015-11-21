using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Types;

namespace Staff.Services
{
    public interface IPersonService
    {
        PagedSearchResult<Person> SearchPersons(
            PersonSearchParameters searchParameters,
            int pageNumber,
            int pageSize );
    }
}