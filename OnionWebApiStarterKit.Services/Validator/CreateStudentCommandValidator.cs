using FluentValidation;
using OnionWebApiStarterKit.Core.Services.Command;

namespace OnionWebApiStarterKit.Services.Validator
{
    /// <summary>
    /// We can also validate against our ViewModels
    /// </summary>
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.FirstMidName).Length(0, 10);
        }
    }
}
