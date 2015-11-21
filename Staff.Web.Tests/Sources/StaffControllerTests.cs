using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;
using RB.Staff.Common.Pub.Stuff;
using RB.Staff.Web.Controllers;
using RB.Staff.Web.Models.Person;

using Staff.Services;
using Staff.Web.Tests.Extensions;

namespace Staff.Web.Tests
{
    [ TestFixture ]
    public class StaffControllerTests
    {
        private Mock<IPersonRepository> _personRepository;
        private IPersonService _personService;
        private List<Person> _persons;
        private const int Id1 = 1, Id2 = 2, Id3 = 3, Id4 = 4, Id5 = 5, Id6 = 6;

        [ SetUp ]
        public void SetUp()
        {
            _persons = new List<Person> {
                CreatePerson( Id1, "Иван", "Программист", 10000, true ),
                CreatePerson( Id2, "Петр", "Программист", 20000, true ),
                CreatePerson( Id3, "Василий", "Программист", 30000, true ),
                CreatePerson( Id4, "Григорий", "Программист", 40000, true ),
                CreatePerson( Id5, "Емельян", "Программист", 15000, true ),
                CreatePerson( Id6, "Алена", "Программист", 25000, false ),
            };

            _personRepository = new Mock<IPersonRepository>( MockBehavior.Strict );
            _personService = new PersonService( _personRepository.Object );
            _personRepository.Setup( x => x.All() ).Returns( _persons.AsQueryable() );
        }

        [ Test, TestCase( null, 6 ), TestCase( true, 5 ), TestCase( false, 1 ) ]
        public void CanFiltePersonsByStatus(
            bool? isActive,
            int expectedResultCount )
        {
            var staffController = new StaffController( _personService );
            var searchParameters = CreateSearchParameters( isActive );

            var result = staffController.List( searchParameters ) as ViewResult;

            Assert.IsNotNull( result );
            var personModel = result.Model as PersonListModel;
            Assert.IsNotNull( personModel );
            Assert.That( personModel.Persons.Count, Is.EqualTo( expectedResultCount ) );
        }

        [ Test,
          TestCase( 1, 6, 1, new[] {Id1, Id2, Id3, Id4, Id5, Id6},
              Description = "Запрос всех элементов на одной странице" ),
          TestCase( 2, 1, 6, new[] {Id2}, Description = "Запрос одного элемента на второй странице" ),
          TestCase( 7, 1, 6, new int[0], Description = "Запрос большего числа элементов, чем имеется " ),
          TestCase( -1, 1, 6, new[] {Id2}, Description = "Запрос с указанием невалидной страницы" ),
          TestCase( 2, 2, 3, new[] {Id3, Id4} ) ]
        public void CanDoPagedPersonSearch(
            int pageNumber,
            int pageSize,
            int expectedTotalPages,
            int[] expextedPersonIds )
        {
            var staffController = new StaffController( _personService );
            var searchParameters = CreateSearchParameters( pageNumber: pageNumber, pageSize: pageSize );

            var result = staffController.List( searchParameters ) as ViewResult;

            Assert.IsNotNull( result );
            var personModel = result.Model as PersonListModel;
            Assert.IsNotNull( personModel );
            Assert.That(
                personModel.TotalPages,
                Is.EqualTo( expectedTotalPages ),
                "Total pages count is not as expected" );
            Assert.That(
                personModel.Persons.Count,
                Is.EqualTo( expextedPersonIds.Length ),
                string.Format( "Expected persons ids are {0}", expextedPersonIds.JoinAsStrings( ";" ) ) );
        }

        #region Routines
        private PersonListModel CreateSearchParameters(
            bool? isActive = null,
            int pageNumber = Constants.FirstPageNumber,
            int? pageSize = null )
        {
            var searchModel = new PersonListModel {
                PageNumber = pageNumber,
                PageSize = pageSize.GetValueOrDefault( _persons.Count ),
                IsActive = isActive
            };
            return searchModel;
        }

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