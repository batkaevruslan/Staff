using System.Collections.Generic;

namespace RB.Staff.Web.Models.Person
{
    public class PersonListModel
    {
        public List<Common.Pub.Entities.Person> Persons { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? IsActive { get; set; }
        public int TotalPages { get; set; }
    }
}