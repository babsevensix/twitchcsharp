public class RubricaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
        public string Telefono { get; set; }
public string Message { get; set; }

    //public ICollection<IndirizzoDto> ListIndirizzi { get; set; }
}

record RequestPersonaDTO(string Cognome, string Nome,
        string Telefono,
        string? Via,
        string? Citta,
        string? Cap );
