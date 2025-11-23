using System.Text.RegularExpressions;
using FluentValidation;

public class RequestPersonaDTOValidator : AbstractValidator<RequestPersonaDTO>
{
    IEntityBaseRepository<PersonaEntity> _repository;
    public RequestPersonaDTOValidator(IEntityBaseRepository<PersonaEntity> repository)
    {
        _repository = repository;

        RuleFor(x=> x.Nome)
            //.NotNull().WithMessage("Il campo nome non puó essere null")
            .NotEmpty().WithMessage("Il campo nome é obbligatorio")
            .MaximumLength(50).WithMessage("Il campo nome non puó superare i 50 caratteri")
            .Must(BeValidNameOrSurname).WithMessage("Inserire il nome senza caratteri speciali");

        RuleFor(x=> x.Cognome)
            .NotEmpty().WithMessage("Il campo cognome é obbligatorio")
            .Must(BeValidNameOrSurname).WithMessage("Inserire il cognome senza caratteri speciali");

        RuleFor(x=>x)
            .Must(CheckDuplicati)
                .WithMessage("Persona giá esistente nel nostro archivio dati")
                .WithName("RequestPersonaDTO");
    }
    

    private bool BeValidNameOrSurname(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return Regex.IsMatch(value, @"^[a-zA-Z0-9_]+$");
        
    }

    private bool CheckDuplicati(RequestPersonaDTO dto)
    {
        var esiste = _repository.All.Any(p => p.Cognome == dto.Cognome && p.Nome == dto.Nome);
        return !esiste;
    }
}