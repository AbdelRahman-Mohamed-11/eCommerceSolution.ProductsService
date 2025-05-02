using ProductsMicroService.BusinessLogic.Dtos;
using ProductsMicroService.BusinessLogic.ServiceContracts;

namespace ProductsMicroService.API.Extensions;

public static class CategoryEndpoints
{
    public static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/categories")
                       .WithTags("Categories");

        group.MapGet("/", async (ICategoryService service, string? search) =>
        {
            var categories = await service.ListAsync(search);
            return Results.Ok(categories);
        })
        .WithName("GetAllCategories")
        .WithSummary("Retrieve all categories")
        .WithDescription("Returns a list of all categories, optionally filtered by search term")
        .WithOpenApi();

        group.MapGet("/{id:guid}", async (ICategoryService service, Guid id) =>
        {
            var category = await service.GetByIdAsync(id);
            return category is null ? Results.NotFound() : Results.Ok(category);
        })
        .WithName("GetCategoryById")
        .WithSummary("Retrieve a single category by ID")
        .WithDescription("Returns a single category by its GUID identifier")
        .WithOpenApi();

        group.MapPost("/", async (ICategoryService service, CategoryCreateDto dto) =>
        {
            var id = await service.CreateAsync(dto);
            var created = await service.GetByIdAsync(id);
            return Results.Created($"/api/categories/{id}", created);
        })
        .WithName("CreateCategory")
        .WithSummary("Create a new category")
        .WithDescription("Creates a new category and returns the created category")
        .WithOpenApi();

        group.MapPut("/{id:guid}", async (ICategoryService service, Guid id, CategoryUpdateDto dto) =>
        {
            if (id != dto.Id)
                return Results.BadRequest();

            var updated = await service.UpdateAsync(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        })
        .WithName("UpdateCategory")
        .WithSummary("Update an existing category")
        .WithDescription("Updates an existing category by its GUID and returns the updated data")
        .WithOpenApi();

        group.MapDelete("/{id:guid}", async (ICategoryService service, Guid id) =>
        {
            var result = await service.DeleteAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteCategory")
        .WithSummary("Delete a category by ID")
        .WithDescription("Deletes the specified category from the system")
        .WithOpenApi();

        return app;
    }
}