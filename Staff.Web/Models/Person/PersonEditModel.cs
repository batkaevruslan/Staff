using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RB.Staff.Web.Models.Person
{
    public class PersonEditModel : PersonCreateModel
    {
        [ HiddenInput( DisplayValue = false ) ]
        public int Id { get; set; }
        [ Display( Name = "Работает", Order = 4 ),
            UIHint("Status")]
        public bool IsActive { get; set; }
    }
}