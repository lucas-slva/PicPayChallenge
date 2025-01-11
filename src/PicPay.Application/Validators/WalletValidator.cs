using FluentValidation;
using PicPay.Application.DTOs;

namespace PicPay.Application.Validators;

public abstract class WalletValidator : AbstractValidator<WalletDto>
{
    protected WalletValidator()
    {
        RuleFor(w => w.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(w => w.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to zero.");
    }
}