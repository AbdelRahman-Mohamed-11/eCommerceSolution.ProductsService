namespace ProductsMicroService.BusinessLogic.Dtos;

public record GetByIdCategoryDto(
    Guid Id,
    string Name
);