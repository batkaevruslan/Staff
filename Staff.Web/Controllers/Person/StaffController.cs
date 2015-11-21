using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RB.Staff.Web.Models.Person;

using Staff.Services;

namespace RB.Staff.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IPersonService _personService;
        public StaffController(IPersonService _personService)
        {
            this._personService = _personService;
        }

        // GET: Staff
        public ActionResult List()
        {
            var model = new PersonListModel();
            model.Persons = _personService.SearchPersons( new PersonSearchParameters(), 1, 1 ).Data;
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
