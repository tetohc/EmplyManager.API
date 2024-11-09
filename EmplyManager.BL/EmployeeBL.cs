using EmplyManager.BL.Configuration;
using EmplyManager.BL.Interfaces;
using EmplyManager.DAL;
using EmplyManager.DAL.Interfaces;
using EmplyManager.Entities.Domain.Employees;
using EmplyManager.Entities.Models;

namespace EmplyManager.BL
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeDAL _employeeDAL;

        public EmployeeBL(IConnectionStringProvider connectionStringProvider)
        {
            _employeeDAL = new EmployeeDAL(connectionStringProvider.GetConnectionString());
        }

        public async Task<OperationResultModel<EmployeeCreateEntity>> Create(EmployeeCreateEntity entity)
        {
            var model = entity.ConvertToDb();
            var result = await _employeeDAL.Create(model);

            if (result.Success)
                return OperationResultModel<EmployeeCreateEntity>.SuccessResult(data: entity);
            else
                return OperationResultModel<EmployeeCreateEntity>.FailureResult(errorMessage: result.ErrorMessage);
        }

        public async Task<OperationResultModel<EmployeeEntity>> Update(EmployeeEntity employeeEntity)
        {
            var model = employeeEntity.ConvertToDb();
            var result = await _employeeDAL.Update(model);

            if (result.Success)
                return OperationResultModel<EmployeeEntity>.SuccessResult(data: employeeEntity);
            else
                return OperationResultModel<EmployeeEntity>.FailureResult(errorMessage: result.ErrorMessage);
        }

        public async Task<OperationResultModel<bool>> Delete(Guid employeeID)
        {
            var result = await _employeeDAL.Delete(employeeID);
            if (result.Success)
                return OperationResultModel<bool>.SuccessResult(data: true);
            else
                return OperationResultModel<bool>.FailureResult(errorMessage: result.ErrorMessage);
        }

        public async Task<OperationResultModel<EmployeeEntity>> GetEmployeeByID(Guid employeeID)
        {
            var result = await _employeeDAL.GetEmployeeByID(employeeID);
            if (result.Success)
                return OperationResultModel<EmployeeEntity>.SuccessResult(data: EmployeeEntity.ConvertToEntity(result.Data));
            else
                return OperationResultModel<EmployeeEntity>.FailureResult(errorMessage: result.ErrorMessage);
        }

        public async Task<IEnumerable<EmployeeListEntity>> GetAll()
        {
            var employees = await _employeeDAL.GetAll();
            return employees.ToList().ConvertAll(x => EmployeeListEntity.ConvertToEntity(x));
        }
    }
}