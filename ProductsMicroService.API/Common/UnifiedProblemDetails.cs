using Microsoft.AspNetCore.Mvc;

namespace ProductsMicroService.API.Common;

public class UnifiedProblemDetails : ProblemDetails
{
    public Dictionary<string, string[]>? ValidationErrors { get; set; }
    public List<string>? Errors { get; set; }
}
