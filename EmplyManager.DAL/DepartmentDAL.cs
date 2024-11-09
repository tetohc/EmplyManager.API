using EmplyManager.DAL.Interfaces;
using EmplyManager.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmplyManager.DAL
{
    public class DepartmentDAL : IDepartmentDAL
    {
        private readonly string _dbConnection;

        public DepartmentDAL(string dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            List<Department> departmentList = new List<Department>();
            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("sp_ListDepartments", connection);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        departmentList.Add(new Department
                        {
                            Id = reader.GetGuid("Id"),
                            Name = reader.GetString("Name").Trim()
                        });
                    }
                }
            }
            return departmentList;
        }
    }
}