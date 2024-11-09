using EmplyManager.Models;

namespace EmplyManager.Entities.Domain.Employees
{
    public class EmployeeCreateEntity
    {
        public Guid DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }

        /// <summary>
        /// This instance method converts the current object to an Employee model suitable for database storage.
        /// </summary>
        /// <returns>A new Employee object with its properties.</returns>
        public Employee ConvertToDb() => new Employee
        {
            Id = Guid.NewGuid(),
            DepartmentID = DepartmentID,
            Name = Name?.Trim(),
            Salary = Salary,
            ContractDate = ContractDate
        };
    }
}