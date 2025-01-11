using FluentValidation;
using PicPay.Application.DTOs;

namespace PicPay.Application.Validators;

public abstract class UserValidator : AbstractValidator<UserDto>
{
    protected UserValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
        
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(user => user.Document)
            .NotEmpty().WithMessage("Document is required.")
            .Length(11, 14).WithMessage("Document must be CPF or CNPJ.");
    }
}