public record GetByIdProductDto(
    Guid Id,
    string Name,
    Guid CategoryId,
    string CategoryName,
    decimal UnitPrice,
    int QuantityInStock
);