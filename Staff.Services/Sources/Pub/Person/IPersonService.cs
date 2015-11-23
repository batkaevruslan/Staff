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

        int Create(
            Person createData );

        Person GetById(
            int id );

        void Update(
            Person person );
    }
}