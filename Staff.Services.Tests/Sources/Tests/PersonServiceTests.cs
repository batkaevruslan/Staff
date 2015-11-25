using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;

using Staff.Services;

namespace RB.Staff.Services.Tests.Tests
{
    [ TestFixture ]
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> _personRepository;
        private IPersonService _personService;
        private List<Person> _persons;
        private const int Id1 = 1, Id2 = 2, Id3 = 3, Id4 = 4, Id5 = 5, Id6 = 6;

        [ SetUp ]
        public void SetUp()
        {
            _persons = new List<Person> {
                CreatePerson( Id1, "Иван", "Программист", 9000, true ), //налог 10% => 900
                CreatePerson( Id2, "Петр", "Программист", 20000, true ), //налог 15% => 3000
                CreatePerson( Id3, "Василий", "Программист", 30000, true ), //налог 25% => 7500
                CreatePerson( Id4, "Григорий", "Программист", 40000, true ), //налог 25% => 10000
                CreatePerson( Id5, "Емельян", "Программист", 15000, true ), //налог 15% => 2250
                CreatePerson( Id6, "Алена", "Программист", 25000, false ), //налог 15% => 6250
            };

            _personRepository = new Mock<IPersonRepository>( MockBehavior.Strict );
            _personService = new PersonService( _personRepository.Object );
            _personRepository.Setup( x => x.All() ).Returns( _persons.AsQueryable() );
        }

        [ Test, Description( "Проверка на то, что генерируется отчет, содержащий данные об активных сотрудниках" ) ]
        public void CanGenerateReportForActivePersons()
        {
            var report = _personService.GenerateReportForActivePersons();

            Assert.That( report.Persons.Count, Is.EqualTo( 5 ), "Report doesn't have write active persons count" );
            Assert.That( report.TotalSalary, Is.EqualTo( 114000 ), "Total salary is wrong" );
            Assert.That( report.TotalTaxes, Is.EqualTo( 23650 ), "Total taxes is wrong" );
            Assert.That( report.TotalTakeHomeSalary, Is.EqualTo( 90350 ), "Total take-home salary is wrong" );
        }

        #region Routines
        private Person CreatePerson(
            int id,
            string name,
            string position,
            decimal salary,
            bool isActive )
        {
            return new Person {
                Id = id,
                Name = name,
                Position = position,
                Salary = salary,
                IsActive = isActive
            };
        }
        #endregion
    }
}