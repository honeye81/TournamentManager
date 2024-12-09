using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.DTOs;

namespace Tournaments.Core.Validators
{
    public class GameForUpdateDtoValidator : AbstractValidator<GameForUpdateDto>
    {
        public GameForUpdateDtoValidator()
        {
            RuleFor(g => g.HomeTeam)
                .NotEmpty()
                .WithMessage("Home team name is required")
                .MaximumLength(50)
                .WithMessage("Home team name cannot exceed 50 characters");

            RuleFor(g => g.AwayTeam)
                .NotEmpty()
                .WithMessage("Away team name is required")
                .MaximumLength(50)
                .WithMessage("Away team name cannot exceed 50 characters")
                .NotEqual(g => g.HomeTeam)
                .WithMessage("Away team must be different from home team");

            RuleFor(g => g.HomeScore)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Home score cannot be negative");

            RuleFor(g => g.AwayScore)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Away score cannot be negative");

            RuleFor(g => g.GameDate)
                .NotEmpty()
                .WithMessage("Game date is required");
        }
    }
}
