public class IndirizzoEntity : Entity
{
    public int Id { get; set; }
    public bool Default { get; set; }
  
    public string Via { get; set; }

    public int LinkCittaId { get; set; }
    public CittaEntity LinkCitta { get; set; }  
}
