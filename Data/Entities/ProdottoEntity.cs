public class ProdottoEntity : Entity, IValidoDalAl
{
    
    public string Name{get;set;}
    public string Description{get;set;}
    public decimal Price{get;set;}
    public int StockQuantity{get;set;}
    public int Discount{get;set;}
    public bool IsOnSale{get;set;}
    public bool IsDigital{get;set;}
    public DateTime ValidoDal{get;set;}
    public DateTime? ValidoAl{get;set;}
}



