using FluentValidation;
using PicPay.Application.DTOs;

namespace PicPay.Application.Validators;

public abstract class TransactionValidator : AbstractValidator<TransactionDto>
{
    protected TransactionValidator()
    {
        RuleFor(t => t.Amount)
            .GreaterThan(0).WithMessage("The transaction amount must be greater than zero.");

        RuleFor(t => t.FromUserId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(t => t.ToUserId)
            .NotEmpty().WithMessage("Receiver ID is required.")
            .NotEqual(t => t.FromUserId).WithMessage("Sender and Receiver cannot be the same.");
    }
}