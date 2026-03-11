using FluentValidation;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}