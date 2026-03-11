using FluentValidation;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Validators
{
    public class TeacherValidator : AbstractValidator<Teacher>
    {
        public TeacherValidator()
        {
            RuleFor(t => t.FirstName)
                .NotEmpty();

            RuleFor(t => t.LastName)
                .NotEmpty();
        }
    }
}