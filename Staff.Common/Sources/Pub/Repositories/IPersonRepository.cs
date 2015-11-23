using System.Linq;

using RB.Staff.Common.Pub.Entities;

namespace RB.Staff.Common.Pub.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<Person> All();

        int Insert(
            Person createData );

        Person GetById(
            int id );

        void Update(
            Person person );
    }
}