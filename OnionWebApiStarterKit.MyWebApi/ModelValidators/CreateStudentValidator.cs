using OnionWebApiStarterKit.Core.DomainModels;
using FluentValidation;
using OnionWebApiStarterKit.MyWebApi.ViewModels;

namespace OnionWebApiStarterKit.MyWebApi.ModelValidators
{
    /// <summary>
    /// We can also validate against our ViewModels
    /// </summary>
    public class CreateStudentValidator : AbstractValidator<CreateOrUpdateStudentViewModel>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.FirstMidName).Length(0, 10);
        }
    }
}