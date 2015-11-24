using System.Collections.Generic;
using System.Linq;

namespace RB.Staff.Common.Pub.Types
{
    public class PersonSalaryReport
    {
        public List<PersonReportItem> Persons { get; private set; }
        public decimal TotalSalary { get; private set; }
        public decimal TotalTaxes { get; private set; }
        public decimal TotalTakeHomeSalary { get; private set; }

        public PersonSalaryReport( List<PersonReportItem> persons )
        {
            Persons = persons;
            TotalSalary = persons.Sum( p => p.Salary );
            TotalTaxes = persons.Sum( p => p.Tax );
            TotalTakeHomeSalary = TotalSalary - TotalTaxes;
        }
    }
}