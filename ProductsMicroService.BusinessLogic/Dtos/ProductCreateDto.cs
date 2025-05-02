public record ProductCreateDto(
    string Name,
    Guid CategoryId,
    decimal UnitPrice,
    int QuantityInStock
);
