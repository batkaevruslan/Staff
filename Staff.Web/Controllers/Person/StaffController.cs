using System;
using System.Web.Mvc;

using RB.Staff.Common.Pub.Stuff;
using RB.Staff.Web.Models.Person;

using Staff.Services;

namespace RB.Staff.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IPersonService _personService;

        public StaffController( IPersonService _personService )
        {
            this._personService = _personService;
        }

        public ActionResult List(bool? isActive, int? pageNumber, int? pageSize )
        {
            var pageNumberFixed = pageNumber.GetValueOrDefault( Constants.FirstPageNumber );
            var pageSizeFixed = pageSize.GetValueOrDefault( PersonListModel.DefaultPageSize );

            var pagedSearchResult = _personService.SearchPersons(
                isActive,
                pageNumberFixed,
                pageSizeFixed);

            var model = new PersonListModel {
                Persons = pagedSearchResult,
                IsActive = isActive,
                PageSize = pageSizeFixed,
                PageNumber = pageNumberFixed
            };
            return View(model);
        }

        /* // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}