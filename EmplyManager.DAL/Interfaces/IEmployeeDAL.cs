using EmplyManager.Entities.Models;
using EmplyManager.Models;

namespace EmplyManager.DAL.Interfaces
{
    public interface IEmployeeDAL
    {
        Task<OperationResultModel<Employee>> Create(Employee employee);

        Task<OperationResultModel<Employee>> Update(Employee employee);

        Task<OperationResultModel<bool>> Delete(Guid employeeID);

        Task<OperationResultModel<Employee>> GetEmployeeByID(Guid employeeID);

        Task<IEnumerable<Employee>> GetAll();
    }
}