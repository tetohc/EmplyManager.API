using EmplyManager.BL.Interfaces;
using EmplyManager.Entities.Domain.Departments;
using Microsoft.AspNetCore.Mvc;

namespace EmplyManager.API.Controllers
{
    /// <summary>
    /// Controller for department management.
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentBL _departmentBL;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="departmentBL">An instance of IDepartmentBL used to handle business logic related to employees.</param>
        public DepartmentsController(IDepartmentBL departmentBL)
        {
            _departmentBL = departmentBL;
        }

        /// <summary>
        /// Retrieve a list of all departments.
        /// </summary>
        /// <returns>Response to the request.</returns>
        [HttpGet("Get-All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DepartmentEntity>))]
        public async Task<IEnumerable<DepartmentEntity>> GetAll() => await _departmentBL.GetAll();
    }
}