using EmplyManager.Entities.Domain.Departments;
using EmplyManager.Models;

namespace EmplyManager.Entities.Domain.Employees
{
    public class EmployeeListEntity
    {
        public Guid Id { get; set; }
        public Guid DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }

        public virtual DepartmentEntity Department { get; set; }

        /// <summary>
        /// This static method converts an Employee model to an EmployeeListEntity.
        /// </summary>
        /// <param name="model">An instance of Employee that needs to be converted.</param>
        /// <returns>A new EmployeeListEntity object with the properties copied from the model.</returns>
        public static EmployeeListEntity ConvertToEntity(Employee model) => new EmployeeListEntity
        {
            Id = model.Id,
            DepartmentID = model.DepartmentID,
            Name = model.Name,
            Salary = model.Salary,
            ContractDate = model.ContractDate,
            Department = DepartmentEntity.ConvertToEntity(model.Department)
        };
    }
}