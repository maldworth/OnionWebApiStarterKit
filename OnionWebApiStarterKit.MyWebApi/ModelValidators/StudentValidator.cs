using OnionWebApiStarterKit.Core.DomainModels;
using FluentValidation;

namespace OnionWebApiStarterKit.MyWebApi.ModelValidators
{
    /// <summary>
    /// Validate against our Domain Model.
    /// </summary>
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.FirstMidName).Length(0, 10);
        }
    }
}