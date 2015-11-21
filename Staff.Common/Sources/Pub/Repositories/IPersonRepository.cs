using System.Linq;

using RB.Staff.Common.Pub.Entities;

namespace RB.Staff.Common.Pub.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<Person> All();
    }
}