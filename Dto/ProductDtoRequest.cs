public record ProductDtoRequest(
    int Id, 
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    int Discount,
    bool IsOnSale,
    bool IsDigital,
    DateTime ValidoDal,
    DateTime? ValidoAl
): IValidoDalAl;

