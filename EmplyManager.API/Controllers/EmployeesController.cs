using EmplyManager.API.Services;
using EmplyManager.BL.Interfaces;
using EmplyManager.Entities.Domain.Employees;
using EmplyManager.Entities.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmplyManager.API.Controllers
{
    /// <summary>
    /// Controller for employee management.
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeBL _employeeBL;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="employeeBL">An instance of IEmployeeBL used to handle business logic related to employees.</param>
        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        /// <summary>
        /// Create a new employee.
        /// </summary>
        /// <param name="entity">Employee object.</param>
        /// <param name="validator">Employee model validator.</param>
        /// <returns>Response to the request.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeCreateEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateEntity entity, [FromServices] IValidator<EmployeeCreateEntity> validator)
        {
            var validate = await validator.ValidateAsync(entity);
            if (!validate.IsValid)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            var result = await _employeeBL.Create(entity);
            if (result.Success)
                return StatusCode(statusCode: StatusCodes.Status201Created,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status201Created, data: result.Data));
            else
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status500InternalServerError, message: result.ErrorMessage));
        }

        /// <summary>
        /// Updates the information of an existing employee.
        /// </summary>
        /// <param name="entity">Model containing the update employee data.</param>
        /// <param name="validator">Validator for the employee update model.</param>
        /// <returns>Response to the request.</returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        public async Task<IActionResult> Update([FromBody] EmployeeEntity entity, [FromServices] IValidator<EmployeeEntity> validator)
        {
            var validate = await validator.ValidateAsync(entity);
            if (!validate.IsValid)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            var employeeResult = await _employeeBL.GetEmployeeByID(entity.Id);
            if (!employeeResult.Success)
                return StatusCode(statusCode: StatusCodes.Status404NotFound,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound,
                    message: employeeResult.ErrorMessage));

            var result = await _employeeBL.Update(entity);
            if (result.Success)
            {
                return StatusCode(statusCode: StatusCodes.Status200OK,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status200OK,
                    data: result.Data));
            }
            else
            {
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status500InternalServerError,
                    message: result.ErrorMessage));
            }
        }

        /// <summary>
        /// Deletes a employee by its ID.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Response to the request.</returns>
        [HttpDelete("Delete/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
        public async Task<IActionResult> Delete(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest,
                    message: "employeeID no puede ser null."));

            var employeeResult = await _employeeBL.GetEmployeeByID(employeeId);
            if (!employeeResult.Success)
                return base.StatusCode(statusCode: StatusCodes.Status404NotFound,
                   value: ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound,
                   message: employeeResult.ErrorMessage));

            var result = await _employeeBL.Delete(employeeId);
            if (result.Success)
            {
                return StatusCode(statusCode: StatusCodes.Status204NoContent,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status204NoContent));
            }
            else
            {
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status500InternalServerError,
                    message: result.ErrorMessage));
            }
        }

        /// <summary>
        /// Retrieves employee information by its ID.
        /// </summary>
        /// <param name="employeeID">Unique identifier of the employee.</param>
        /// <returns>Response to the request.</returns>
        [HttpGet("Get-by-Id/{employeeID}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeEntity))]
        public async Task<IActionResult> GetEmployeeByID(Guid employeeID)
        {
            if (employeeID == Guid.Empty)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest,
                    message: "employeeID no puede ser null."));

            var result = await _employeeBL.GetEmployeeByID(employeeID);
            if (result.Success)
            {
                return StatusCode(statusCode: StatusCodes.Status200OK,
                    value: ResponseApiService.Response(statusCode: StatusCodes.Status200OK,
                    data: result.Data));
            }
            else
            {
                return StatusCode(statusCode: StatusCodes.Status404NotFound,
                   value: ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound,
                   message: result.ErrorMessage));
            }
        }

        /// <summary>
        /// Retrieve a list of all employees.
        /// </summary>
        /// <returns>Response to the request.</returns>
        [HttpGet("Get-All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeListEntity>))]
        public async Task<IEnumerable<EmployeeListEntity>> GetAll() => await _employeeBL.GetAll();
    }
}