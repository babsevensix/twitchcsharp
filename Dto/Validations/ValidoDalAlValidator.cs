using FluentValidation;

public class ValidoDalAlValidator: AbstractValidator<IValidoDalAl>
{
    public ValidoDalAlValidator()
    {
        RuleFor(x=> x.ValidoDal)
            .NotNull().WithMessage("Indicare la data di partenza di validitá del prodotto")
            .LessThanOrEqualTo(x=> x.ValidoAl.GetValueOrDefault(DateTime.MaxValue))
                .WithMessage("La data di partenza deve essere inferiore alla data di fine validitá del prodotto")
            ;

        RuleFor(x=>x.ValidoAl)
            .GreaterThanOrEqualTo(x=>x.ValidoDal)
                .When(x=> x.ValidoAl.HasValue)
                .WithMessage("La data di scadenza deve essere maggiore della data di inizio validitá del prodotto");

    }
}

public static class ValidoDalAlValidatorExtension
{
    public static IRuleBuilderOptions<T, IValidoDalAl> ValidoDalAl<T>(
        this IRuleBuilder<T, IValidoDalAl> ruleBuilder
        )
    {
        return ruleBuilder.SetValidator(new ValidoDalAlValidator());

    }
}