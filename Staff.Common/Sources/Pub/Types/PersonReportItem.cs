namespace RB.Staff.Common.Pub.Types
{
    public class PersonReportItem
    {
        public string Name { get; private set; }
        public decimal Salary { get; private set; }
        public decimal Tax { get; private set; }
        public decimal TakeHomeSalary { get; private set; }

        public PersonReportItem(
            string name,
            decimal salary )
        {
            Name = name;
            Salary = salary;
            decimal tax = CalculateTax( salary );
            Tax = tax;
            TakeHomeSalary = salary - tax;
        }

        private decimal CalculateTax(
            decimal salary )
        {
            if( salary < 10000 ) {
                return salary*0.1m;
            }
            if( salary > 25000 ) {
                return salary*0.25m;
            }
            return salary*0.15m;
        }
    }
}