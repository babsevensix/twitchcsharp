using Mapster;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

public static class ProdottiEndpoints
{
    
    public static void AddEndpoints(this WebApplication app)
    {
        app.MapPost("/prodotto", (ProductDtoRequest req) =>
        {
            return Results.Ok();
        }).AddFluentValidationAutoValidation();

        app.MapGet("/prodotto", (IEntityBaseRepository<ProdottoEntity> repository) =>
        {
            return repository.All.ProjectToType<ProductDtoRequest>();

        });
        
        app.MapGet("/prodotto/validiora", (IEntityBaseRepository<ProdottoEntity> repository) =>
        {
            return repository.All
                .ValidoOra()
                .ProjectToType<ProductDtoRequest>();
            
        });
    }
}