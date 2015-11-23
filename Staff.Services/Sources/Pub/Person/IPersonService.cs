using PagedList;

using RB.Staff.Common.Pub.Entities;

namespace Staff.Services
{
    public interface IPersonService
    {
        IPagedList<Person> SearchPersons(
            bool? isActive,
            int pageNumber,
            int pageSize );
    }
}