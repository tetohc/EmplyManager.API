using EmplyManager.BL;
using EmplyManager.BL.Configuration;
using EmplyManager.BL.Interfaces;
using EmplyManager.BL.Validators.Employees;
using EmplyManager.DAL;
using EmplyManager.DAL.Interfaces;
using EmplyManager.Entities.Domain.Employees;
using FluentValidation;

namespace EmplyManager.API.Services
{
    /// <summary>
    /// This static class is responsible for configuring dependency injection for the application.
    /// </summary>
    public static class DependencyInjectionService
    {
        /// <summary>
        /// Registers the necessary services for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to which the dependencies are added.</param>
        public static void AddDependenciesConfiguration(this IServiceCollection services)
        {
            #region Connection string

            services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();

            #endregion Connection string

            #region Employee

            services.AddScoped<IEmployeeDAL>(provider =>
            {
                var connectionStringProvider = provider.GetRequiredService<IConnectionStringProvider>();
                return new EmployeeDAL(connectionStringProvider.GetConnectionString());
            });
            services.AddScoped<IEmployeeBL, EmployeeBL>();

            #endregion Employee

            #region Department

            services.AddScoped<IDepartmentDAL>(provider =>
            {
                var connectionStringProvider = provider.GetRequiredService<IConnectionStringProvider>();
                return new DepartmentDAL(connectionStringProvider.GetConnectionString());
            });
            services.AddScoped<IDepartmentBL, DepartmentBL>();

            #endregion Department

            #region Validators

            services.AddScoped<IValidator<EmployeeCreateEntity>, EmployeeCreateValidator>();
            services.AddScoped<IValidator<EmployeeEntity>, EmployeeUpdateValidator>();

            #endregion Validators
        }
    }
}