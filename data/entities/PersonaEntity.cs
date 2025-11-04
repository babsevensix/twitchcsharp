public class PersonaEntity
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Cognome { get; set; }

    public string Telefono { get; set; }

    public ICollection<IndirizzoEntity> ListIndirizzi { get; set; }

}

public class IndirizzoEntity
{
    public int Id { get; set; }
    public bool Default { get; set; }
  
    public string Via { get; set; }

    public CittaEntity LinkCitta { get; set; }  
}

public class CittaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public string Cap { get; set; }
}