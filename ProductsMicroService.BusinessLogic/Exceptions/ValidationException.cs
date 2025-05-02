namespace ProductsMicroService.BusinessLogic.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}