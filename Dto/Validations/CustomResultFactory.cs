using FluentValidation.Results;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IResult CreateResult(
            EndpointFilterInvocationContext context, 
            ValidationResult validationResult)
    {
        var errors = validationResult.Errors
            .GroupBy(e=>e.PropertyName)
            .Select(grp => new { 
                PropertyName = grp.Key, 
                ErrorMessage = grp.Select( m => m.ErrorMessage).Aggregate((acc, el)=> $"{acc}, {el}")
            });

        return TypedResults.BadRequest(errors);
    }
}