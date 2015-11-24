using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using PagedList;

namespace RB.Staff.Web.Models.Person
{
    public class PersonListModel
    {
        public const int DefaultPageSize = 10;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? IsActive { get; set; }
        public IPagedList<PersonEditModel> Persons { get; set; }

        public List<SelectListItem> GetPageSizes()
        {
            return new[] {DefaultPageSize, DefaultPageSize*2, DefaultPageSize*5}.Select(
                x => new SelectListItem {
                    Selected = x == PageNumber,
                    Value = x.ToString(),
                    Text = x.ToString()
                } ).ToList();
        }
    }
}