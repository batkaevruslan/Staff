using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

using RB.Staff.Common.Pub.Entities;

namespace Staff.Data.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StaffDbContext>
    {
        private static readonly List<string> _names = new List<string> {
            "Авксентий",
            "Агап",
            "Агафон",
            "Александр",
            "Алексей",
            "Анатолий",
            "Андрей",
            "Андрон",
            "Анисим",
            "Антип",
            "Антон",
            "Ануфрий",
            "Анфим",
            "Аристарх",
            "Аркадий",
            "Арсен",
            "Арсений",
            "Артамон",
            "Артем",
            "Архип",
            "Афанасий",
            "Василий",
            "Виссарион",
            "Влас",
            "Галактион",
            "Геннадий",
            "Георгий",
            "Герасим",
            "Гордей",
            "Григорий",
            "Демид",
            "Демьян",
            "Денис",
            "Дмитрий",
            "Дорофей",
            "Евгений",
            "Евграф",
            "Евдоким",
            "Евлампий",
            "Евсей",
            "Евстафий",
            "Евстигней",
            "Евстрат",
            "Егор",
            "Емельян",
            "Епифан",
            "Ермолай",
            "Ерофей",
            "Ефим",
            "Зиновий"
        };
        private static readonly List<string> _positions = new List<string> {
            "Программист",
            "Тестировщик",
            "Менеджер"
        };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(
            StaffDbContext context )
        {
            var rnd = new Random();
            if( !context.Persons.Any() ) {
                foreach( var name in _names ) {
                    var position = _positions[ rnd.Next( 3 ) ];
                    var salary = rnd.Next( 40*1000 );
                    var isActive = rnd.Next( 2 ) == 1;
                    var newPerson = new Person {
                        Name = name,
                        Position = position,
                        IsActive = isActive,
                        Salary = salary
                    };
                    context.Persons.Add( newPerson );
                }
            }
            context.SaveChanges();
        }
    }
}