using EmplyManager.Models;

namespace EmplyManager.Entities.Domain.Employees
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
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
            Id = Id,
            DepartmentID = DepartmentID,
            Name = Name?.Trim(),
            Salary = Salary,
            ContractDate = ContractDate
        };

        /// <summary>
        /// This static method converts an Employee model to an EmployeeEntity.
        /// </summary>
        /// <param name="model">An instance of Employee that needs to be converted.</param>
        /// <returns>A new EmployeeEntity object with the properties copied from the model.</returns>
        public static EmployeeEntity ConvertToEntity(Employee model) => new EmployeeEntity
        {
            Id = model.Id,
            DepartmentID = model.DepartmentID,
            Name = model.Name.Trim(),
            Salary = model.Salary,
            ContractDate = model.ContractDate
        };
    }
}