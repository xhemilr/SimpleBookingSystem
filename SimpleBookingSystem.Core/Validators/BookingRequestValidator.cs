using FluentValidation;
using SimpleBookingSystem.Core.Requests;

namespace SimpleBookingSystem.Core.Validators
{
    public class BookingRequestValidator : AbstractValidator<BookingRequest>
    {
        public BookingRequestValidator()
        {
            RuleFor(req => req.DateFrom)
                .NotNull().WithMessage("Date from cannot be null!")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Resource cannot be booked in the past!");
            RuleFor(req => req.DateTo)
                .NotNull().WithMessage("Date from cannot be null!")
                .GreaterThan(req => req.DateFrom).WithMessage("Date to must be greater than date from!");
            RuleFor(req => req.ResourceId)
                .NotNull().WithMessage("Resource cannot be null!")
                .GreaterThanOrEqualTo(1);
            RuleFor(req => req.Quantity)
                .NotNull().WithMessage("Quantity must have a value!")
                .GreaterThanOrEqualTo(1);
        }
    }
}
