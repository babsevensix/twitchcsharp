public class IndirizzoDto
{
    public int Id { get; set; }
    //public bool Default { get; set; }

    public string Via { get; set; }

    // public int LinkCittaId { get; set; }
    // public CittaEntity LinkCitta { get; set; }  
    public string LinkCittaNome { get; set; }
}

public record NomeCittaDto(int Id, string LinkCittaNome);