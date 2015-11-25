using System.ComponentModel.DataAnnotations;

using NUnit.Framework;

using RB.Staff.Web.Models.Person;

namespace RB.Staff.Web.Tests.Sources.Tests
{
    [ TestFixture ]
    public class PersonValidationTests
    {
        [ Test, TestCase( null, "QA", 1 ), TestCase( "Ivan", null, 1 ), TestCase("Ivan", "QA", -1 ) ]
        public void CanNotCreatePersonWithInvalidData(
            string name,
            string position,
            decimal salary )
        {
            var createModel = new PersonCreateModel() {
                Name = name,
                Position = position,
                Salary = salary
            };
            var context = new ValidationContext( createModel, null, null ) {
                DisplayName = createModel.GetType().Name,
                
            };

            Assert.Throws<ValidationException>( () => Validator.ValidateObject( createModel, context, true ) );
        }
    }
}