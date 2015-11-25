using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Repositories;
using RB.Staff.Web.Controllers;
using RB.Staff.Web.Models.Person;
using RB.Staff.Web.Tests.Sources.Pvt.Extensions;

using Staff.Services;

namespace RB.Staff.Web.Tests.Sources.Tests
{
    [ TestFixture ]
    public class PersonControllerTests
    {
        private IPersonRepository _personRepository;
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

            var personRepositoryMock = new Mock<IPersonRepository>( MockBehavior.Strict );
            personRepositoryMock.Setup(x => x.All()).Returns(_persons.AsQueryable());
            _personRepository = personRepositoryMock.Object;
            _personService = new PersonService(_personRepository);
        }

        [ Test, TestCase( null, 6 ), TestCase( true, 5 ), TestCase( false, 1 ) ]
        public void CanFiltePersonsByStatus(
            bool? isActive,
            int expectedResultCount )
        {
            var staffController = new PersonController( _personService );

            var result = staffController.List( isActive, null, null ) as ViewResult;

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
          TestCase( 2, 2, 3, new[] {Id3, Id4} ) ]
        public void CanDoPagedPersonSearch(
            int pageNumber,
            int pageSize,
            int expectedTotalPages,
            int[] expextedPersonIds )
        {
            var staffController = new PersonController( _personService );

            var result = staffController.List( null, pageNumber, pageSize ) as ViewResult;

            Assert.IsNotNull( result );
            var personModel = result.Model as PersonListModel;
            Assert.IsNotNull( personModel );
            Assert.That(
                personModel.Persons.PageCount,
                Is.EqualTo( expectedTotalPages ),
                "Total pages count is not as expected" );
            Assert.That(
                personModel.Persons.Count,
                Is.EqualTo( expextedPersonIds.Length ),
                string.Format( "Expected persons ids are {0}", expextedPersonIds.JoinAsStrings( ";" ) ) );
        }
        
        [ Test ]
        public void CanDownloadFile()
        {
            var staffController = new PersonController( _personService );

            var actionResult = staffController.DownloadReport();

            Assert.IsNotNull( actionResult );
            var fileContentResult = actionResult as FileContentResult;
            Assert.That( fileContentResult.FileContents, Is.Not.Empty, "File is empty" );
            Assert.That( fileContentResult.FileDownloadName, Is.Not.Empty, "File name is empty" );
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