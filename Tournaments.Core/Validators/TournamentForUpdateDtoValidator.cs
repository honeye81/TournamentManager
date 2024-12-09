using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.DTOs;

namespace Tournaments.Core.Validators
{
    public class TournamentForUpdateDtoValidator : AbstractValidator<TournamentForUpdateDto>
    {
        public TournamentForUpdateDtoValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("Tournament name is required")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters");

            RuleFor(t => t.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required");

            RuleFor(t => t.EndDate)
                .NotEmpty()
                .WithMessage("End date is required")
                .GreaterThanOrEqualTo(t => t.StartDate)
                .WithMessage("End date must be after or equal to start date");
        }
    }
}
