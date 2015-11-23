using System.ComponentModel.DataAnnotations;

namespace RB.Staff.Web.Models.Person
{
    public class PersonEditModel : PersonCreateModel
    {
        [ Display( Name = "Работает", Order = 4) ]
        public bool IsActive { get; set; }
    }
}