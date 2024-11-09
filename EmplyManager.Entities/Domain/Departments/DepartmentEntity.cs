using EmplyManager.Models;

namespace EmplyManager.Entities.Domain.Departments
{
    public class DepartmentEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// This static method converts a Department model to a DepartmentEntity.
        /// </summary>
        /// <param name="model">An instance of Department that needs to be converted.</param>
        /// <returns>A new DepartmentEntity object with the Id and Name properties copied from the model.</returns>
        public static DepartmentEntity ConvertToEntity(Department model) => new DepartmentEntity
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}