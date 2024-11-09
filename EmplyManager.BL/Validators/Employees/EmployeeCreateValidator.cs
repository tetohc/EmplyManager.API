using EmplyManager.Entities.Domain.Employees;
using FluentValidation;

namespace EmplyManager.BL.Validators.Employees
{
    public class EmployeeCreateValidator : AbstractValidator<EmployeeCreateEntity>
    {
        public EmployeeCreateValidator()
        {
            RuleFor(x => x.DepartmentID).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Salary).GreaterThan(0);
            RuleFor(x => x.ContractDate).NotEmpty();
        }
    }
}