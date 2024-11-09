using EmplyManager.Entities.Domain.Employees;
using EmplyManager.Entities.Models;

namespace EmplyManager.BL.Interfaces
{
    public interface IEmployeeBL
    {
        Task<OperationResultModel<EmployeeCreateEntity>> Create(EmployeeCreateEntity entity);

        Task<OperationResultModel<EmployeeEntity>> Update(EmployeeEntity entity);

        Task<OperationResultModel<bool>> Delete(Guid employeeID);

        Task<OperationResultModel<EmployeeEntity>> GetEmployeeByID(Guid employeeID);

        Task<IEnumerable<EmployeeListEntity>> GetAll();
    }
}