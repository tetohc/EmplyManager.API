using EmplyManager.BL.Configuration;
using EmplyManager.BL.Interfaces;
using EmplyManager.DAL;
using EmplyManager.DAL.Interfaces;
using EmplyManager.Entities.Domain.Departments;

namespace EmplyManager.BL
{
    public class DepartmentBL : IDepartmentBL
    {
        private readonly IDepartmentDAL _departmentDAL;

        public DepartmentBL(IConnectionStringProvider connectionStringProvider)
        {
            _departmentDAL = new DepartmentDAL(connectionStringProvider.GetConnectionString());
        }

        public async Task<IEnumerable<DepartmentEntity>> GetAll()
        {
            var departmentList = await _departmentDAL.GetAll();
            return departmentList.ToList().ConvertAll(x => DepartmentEntity.ConvertToEntity(x));
        }
    }
}