using FluentValidation;

public class ProductDtoRequestValidator: AbstractValidator<ProductDtoRequest>
{
    public ProductDtoRequestValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Il nome prodotto deve essere valorizzato")
            .MaximumLength(100).WithMessage("Il nome del prodotto supera i 100 caratteri");

        RuleFor(x=>x.Discount)
            .GreaterThan(0)
                .When(x=>x.IsOnSale)
                .WithMessage("Lo sconto deve essere indicato per i prodotti in offerta");
    
        RuleFor(x=>x.StockQuantity)
            .GreaterThan(0)
                .Unless(x=>x.IsDigital)
                .WithMessage("Per prodotti non digitali indicare la quantitá a magazzino");

        //Include(new ValidoDalAlValidator());
        RuleFor(x=>x as IValidoDalAl).ValidoDalAl();
        
    }
}