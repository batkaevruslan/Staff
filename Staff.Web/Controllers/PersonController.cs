﻿using System.Web.Mvc;

using RB.Staff.Common.Pub.Entities;
using RB.Staff.Common.Pub.Stuff;
using RB.Staff.Web.Models.Person;

using Staff.Services;

namespace RB.Staff.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController( IPersonService _personService )
        {
            this._personService = _personService;
        }

        public ActionResult List(
            bool? isActive,
            int? pageNumber,
            int? pageSize )
        {
            var pageNumberFixed = pageNumber.GetValueOrDefault( Constants.FirstPageNumber );
            var pageSizeFixed = pageSize.GetValueOrDefault( PersonListModel.DefaultPageSize );

            var pagedSearchResult = _personService.SearchPersons( isActive, pageNumberFixed, pageSizeFixed );

            var model = new PersonListModel {
                Persons = pagedSearchResult,
                IsActive = isActive,
                PageSize = pageSizeFixed,
                PageNumber = pageNumberFixed
            };
            return View( model );
        }

        /* // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }*/

        [ HttpGet ]
        public ActionResult Create()
        {
            return View();
        }

        [ HttpPost ]
        public ActionResult Create(
            PersonCreateModel createModel )
        {
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

            var personModel = new PersonEditModel {
                Name = person.Name,
                Position = person.Position,
                Salary = person.Salary,
                IsActive = person.IsActive
            };
            return View( personModel );
        }

        [ HttpPost ]
        public ActionResult Edit(
            int id,
            PersonEditModel editModel )
        {
            try {
                var person = _personService.GetById( id );
                if( person == null ) {
                    return RedirectToAction( "List" );
                }

                person.Name = editModel.Name;
                person.Position = editModel.Position;
                person.IsActive = editModel.IsActive;
                person.Salary = editModel.Salary;

                _personService.Update( person );

                return RedirectToAction( "List" );
            }
            catch {
                return View();
            }
        }
    }
}