namespace ProductsMicroService.BusinessLogic.Dtos;

public record ListProductDto(
    Guid Id,
    string Name,
    string CategoryName,
    decimal UnitPrice,
    int QuantityInStock
);