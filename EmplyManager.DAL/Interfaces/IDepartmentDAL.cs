using EmplyManager.Models;

namespace EmplyManager.DAL.Interfaces
{
    public interface IDepartmentDAL
    {
        Task<IEnumerable<Department>> GetAll();
    }
}