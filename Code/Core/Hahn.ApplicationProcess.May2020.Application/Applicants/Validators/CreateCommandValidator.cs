using FluentValidation;
using Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Create;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Constants;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.Validators
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        private readonly ICountryFinder _countryFinder;
        public CreateCommandValidator(ICountryFinder countryFinder)
        {
            _countryFinder = countryFinder;

            RuleFor(x => x.Name).NotEmpty().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.FamilyName).NotEmpty().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(10).MaximumLength(500);
            RuleFor(x => x.Age).NotNull().ExclusiveBetween(20, 60);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.Hired).NotNull();

            RuleFor(x => x.CountryOfOrigin).MustAsync((name, cancellation) => IsValidCountry(name)).
                WithMessage(AppConstants.ErrorMessages.InvalidCountry);
        }

        private async Task<bool> IsValidCountry(string countryName)
        {
            return await _countryFinder.FindAsync(countryName);
        }
    }
}
