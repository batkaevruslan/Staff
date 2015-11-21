namespace Staff.Common
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }

        public Person( string name,
            string position,
            int salary,
            bool isActive )
        {
            Name = name;
            Position = position;
            Salary = salary;
            IsActive = isActive;
        }
    }
}