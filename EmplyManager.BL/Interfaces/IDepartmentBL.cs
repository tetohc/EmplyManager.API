using EmplyManager.Entities.Domain.Departments;

namespace EmplyManager.BL.Interfaces
{
    public interface IDepartmentBL
    {
        Task<IEnumerable<DepartmentEntity>> GetAll();
    }
}