using FluentValidation;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(s => s.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(s => s.Age)
                .GreaterThan(0);

            RuleFor(s => s.Grade)
                .NotEmpty();
        }
    }
}