public record ProductUpdateDto(
    Guid Id,
    string Name,
    Guid CategoryId,
    decimal UnitPrice,
    int QuantityInStock
);