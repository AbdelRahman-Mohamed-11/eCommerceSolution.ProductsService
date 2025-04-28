namespace ProductsMicroService.BusinessLogic.Entities;

public class Product
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int QuantityInStock { get; set; }
}
