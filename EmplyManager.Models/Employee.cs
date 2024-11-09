namespace EmplyManager.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }

        public virtual Department Department { get; set; }
    }
}