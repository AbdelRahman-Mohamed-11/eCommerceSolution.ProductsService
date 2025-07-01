using ProductsMicroService.BusinessLogic.Dtos;
using ProductsMicroService.BusinessLogic.ServiceContracts;

namespace ProductsMicroService.API.Extensions;

public static class ProductEndpoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
                       .WithTags("Products"); 

        group.MapGet("/", async (IProductService service, string? search) =>
        {
            var result = await service.ListAsync(search);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(result.Error, statusCode: result.StatusCode);
        })
        .WithName("GetAllProducts")
        .WithSummary("Retrieve all products")
        .WithDescription("Returns a list of all products from the product service")
        .WithOpenApi();

        group.MapGet("/{id:guid}", async (IProductService service, Guid id) =>
        {
            var result = await service.GetByIdAsync(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(result.Error, statusCode: result.StatusCode);
        })
        .WithName("GetProductById")
        .WithSummary("Retrieve a single product by ID")
        .WithDescription("Returns a single product by its GUID identifier")
        .WithOpenApi();

        group.MapPost("/", async (IProductService service, ProductCreateDto dto) =>
        {
            var result = await service.CreateAsync(dto);
            if (!result.IsSuccess)
                return Results.Problem(result.Error, statusCode: result.StatusCode);

            var getResult = await service.GetByIdAsync(result.Value);
            return getResult.IsSuccess
                ? Results.Created($"/api/products/{result.Value}", getResult.Value)
                : Results.Problem(getResult.Error, statusCode: getResult.StatusCode);
        })
        .WithName("CreateProduct")
        .WithSummary("Create a new product")
        .WithDescription("Creates a new product and returns the created product")
        .WithOpenApi();

        group.MapPut("/{id:guid}", async (IProductService service, Guid id, ProductUpdateDto dto) =>
        {
            if (id != dto.Id)
                return Results.BadRequest("ID mismatch between route and body");

            var result = await service.UpdateAsync(dto);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ValidationErrors is not null
                    ? Results.ValidationProblem(result.ValidationErrors)
                    : Results.Problem(result.Error, statusCode: result.StatusCode);
        })
        .WithName("UpdateProduct")
        .WithSummary("Update an existing product")
        .WithDescription("Updates an existing product by its GUID and returns the updated data")
        .WithOpenApi();

        group.MapDelete("/{id:guid}", async (IProductService service, Guid id) =>
        {
            var result = await service.DeleteAsync(id);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.Problem(result.Error, statusCode: result.StatusCode);
        })
        .WithName("DeleteProduct")
        .WithSummary("Delete a product by ID")
        .WithDescription("Deletes the specified product from the system")
        .WithOpenApi();

        return app;
    }
}
