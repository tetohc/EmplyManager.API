using EmplyManager.DAL.Interfaces;
using EmplyManager.Entities.Models;
using EmplyManager.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmplyManager.DAL
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly string _dbConnection;

        public EmployeeDAL(string dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<OperationResultModel<Employee>> Create(Employee employee)
        {
            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("sp_AddEmployee", connection, transaction);
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name?.Trim());
                    command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@ContractDate", employee.ContractDate);
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            return OperationResultModel<Employee>.SuccessResult(data: employee);
                        }
                        else
                        {
                            transaction.Rollback();
                            return OperationResultModel<Employee>.FailureResult(errorMessage: "No se pudo insertar el empleado.");
                        }
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return OperationResultModel<Employee>.FailureResult(errorMessage: "Ha ocurrido un error inesperado mientras se insertaba el empleado.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return OperationResultModel<Employee>.FailureResult(errorMessage: "Ha ocurrido un error inesperado mientras se procesaba la solicitud.");
                    }
                }
            }
        }

        public async Task<OperationResultModel<Employee>> Update(Employee employee)
        {
            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("sp_UpdateEmployee", connection, transaction);
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name?.Trim());
                    command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@ContractDate", employee.ContractDate);
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            return OperationResultModel<Employee>.SuccessResult(data: employee);
                        }
                        else
                        {
                            transaction.Rollback();
                            return OperationResultModel<Employee>.FailureResult(errorMessage: "No se pudo actualizar el empleado.");
                        }
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return OperationResultModel<Employee>.FailureResult(errorMessage: "Ha ocurrido un error inesperado mientras se actualizaba el empleado.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return OperationResultModel<Employee>.FailureResult(errorMessage: "Ha ocurrido un error inesperado mientras se procesaba la solicitud.");
                    }
                }
            }
        }

        public async Task<OperationResultModel<bool>> Delete(Guid employeeID)
        {
            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("sp_DeleteEmployee", connection, transaction);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            return OperationResultModel<bool>.SuccessResult(data: true);
                        }
                        else
                        {
                            transaction.Rollback();
                            return OperationResultModel<bool>.FailureResult(errorMessage: "No se pudo eliminar el empleado.");
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return OperationResultModel<bool>.FailureResult(errorMessage: "Ha ocurrido un error inesperado mientras se eliminaba el empleado.");
                    }
                }
            }
        }

        public async Task<OperationResultModel<Employee>> GetEmployeeByID(Guid employeeID)
        {
            Employee? employee = null;
            try
            {
                using (var connection = new SqlConnection(_dbConnection))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("sp_GetEmployeeById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier)).Value = employeeID;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            employee = new Employee()
                            {
                                Id = reader.GetGuid("Id"),
                                DepartmentID = reader.GetGuid("DepartmentID"),
                                Name = reader.GetString("Name").Trim(),
                                Salary = reader.GetDecimal("Salary"),
                                ContractDate = reader.GetDateTime("ContractDate")
                            };
                        }
                    }
                }

                if (employee != null)
                    return OperationResultModel<Employee>.SuccessResult(data: employee);
                else
                    return OperationResultModel<Employee>.FailureResult(errorMessage: "El empleado no fue encontrado.");
            }
            catch (Exception)
            {
                return OperationResultModel<Employee>.FailureResult(errorMessage: "Ha ocurrido un error inesperado mientras se obtenia el empleado.");
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("sp_ListEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee
                        {
                            Id = reader.GetGuid("Id"),
                            Name = reader.GetString("Name").Trim(),
                            Salary = reader.GetDecimal("Salary"),
                            ContractDate = DateTime.Parse(reader.GetString("ContractDate")),
                            Department = new Department
                            {
                                Id = reader.GetGuid("DepartmentId"),
                                Name = reader.GetString("DepartmentName").Trim()
                            }
                        });
                    }
                }
            }
            return list;
        }
    }
}