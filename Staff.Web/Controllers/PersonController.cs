﻿using System.Linq;
using System.Web.Mvc;

using PagedList;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Stuff;
using RB.Staff.Web.Helpers;
using RB.Staff.Web.Models.Person;

using Staff.Services;

namespace RB.Staff.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController( IPersonService personService )
        {
            this._personService = personService;
        }

        public ActionResult List(
            bool? isActive,
            int? pageNumber,
            int? pageSize )
        {
            var pageNumberFixed = pageNumber.GetValueOrDefault( Constants.FirstPageNumber );
            var pageSizeFixed = pageSize.GetValueOrDefault( PersonListModel.DefaultPageSize );

            var pagedSearchResult = _personService.SearchPersons( isActive, pageNumberFixed, pageSizeFixed );
            var personEditModelPagedList =
                new StaticPagedList<PersonEditModel>(
                    pagedSearchResult.Select( ConvertToPersonEditModel ),
                    pagedSearchResult.PageNumber,
                    pagedSearchResult.PageSize,
                    pagedSearchResult.TotalItemCount );
            var model = new PersonListModel {
                Persons = personEditModelPagedList,
                IsActive = isActive,
                PageSize = pageSizeFixed,
                PageNumber = pageNumberFixed
            };
            return View( model );
        }

        [ HttpGet ]
        public ActionResult Create()
        {
            return View();
        }

        [ HttpPost ]
        public ActionResult Create(
            PersonCreateModel createModel )
        {
            if( !ModelState.IsValid ) {
                return View( "Create", createModel );
            }

            var createData = new Person {
                Position = createModel.Position,
                Name = createModel.Name,
                Salary = createModel.Salary,
                IsActive = true
            };
            _personService.Create( createData );
            return RedirectToAction( "List" );
        }

        public ActionResult Edit(
            int id )
        {
            var person = _personService.GetById( id );
            if( person == null ) {
                return RedirectToAction( "List" );
            }

            PersonEditModel personModel = ConvertToPersonEditModel( person );
            return View( personModel );
        }

        [ HttpPost ]
        public ActionResult Edit(
            int id,
            PersonEditModel editModel )
        {
            var person = _personService.GetById( id );
            if( person == null ) {
                return RedirectToAction( "List" );
            }
            if( !ModelState.IsValid ) {
                return View( "Create", editModel );
            }
            person.Name = editModel.Name;
            person.Position = editModel.Position;
            person.IsActive = editModel.IsActive;
            person.Salary = editModel.Salary;

            _personService.Update( person );

            return RedirectToAction( "List" );
        }

        public ActionResult DownloadReport()
        {
            var report = _personService.GenerateReportForActivePersons();
            byte[] fileContents = PersonSalaryReportSerializer.SerializeReport( report );
            return File( fileContents, "test/plain", "report.txt" );
        }

        #region Routines
        private static PersonEditModel ConvertToPersonEditModel(
            Person person )
        {
            return new PersonEditModel {
                Id = person.Id,
                Name = person.Name,
                Position = person.Position,
                IsActive = person.IsActive,
                Salary = person.Salary
            };
        }
        #endregion
    }
}