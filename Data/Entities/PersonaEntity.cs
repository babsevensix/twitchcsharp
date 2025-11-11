public class PersonaEntity: IEntity
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Cognome { get; set; }

    public string Telefono { get; set; }

    public ICollection<IndirizzoEntity> ListIndirizzi { get; set; }

}
