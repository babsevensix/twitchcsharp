using System.ComponentModel.DataAnnotations;

public class RubricaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Telefono { get; set; }
    public string Message { get; set; }

    //public ICollection<IndirizzoDto> ListIndirizzi { get; set; }
}

public record RequestPersonaDTO(string Cognome, string Nome,
        string Telefono,
        string? Via,
        string? Citta,
        string? Cap );

// public record RequestPersonaDTO
// {
//     [Required(ErrorMessage = "Il campo cognome é un valore obbligatorio")]
//     public string Cognome { get; set; }

//     [Required(ErrorMessage = "Il campo nome é un valore obbligatorio")]
//     public string Nome { get; set; }
//     public string Via { get; set; }
//     public string Citta { get; set; }
//     public string Cap { get; set; }
// }
