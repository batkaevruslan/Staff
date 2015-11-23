using System.ComponentModel.DataAnnotations;

namespace RB.Staff.Web.Models.Person
{
    public class PersonCreateModel
    {
        [ Required, Display( Name = "Имя", Order = 1 ) ]
        public string Name { get; set; }
        [ Required, Display( Name = "Должность", Order = 2 ) ]
        public string Position { get; set; }
        [ Display( Name = "Зарплата", Order = 3 ), DataType( DataType.Currency ) ]
        public decimal Salary { get; set; }
    }
}