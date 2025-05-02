namespace ProductsMicroService.BusinessLogic.common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public Dictionary<string, string[]>? ValidationErrors { get; }
    public T? Value { get; }
    public int StatusCode { get; }

    private Result(bool isSuccess, string error, T? value, int statusCode, Dictionary<string, string[]>? validationErrors = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
        StatusCode = statusCode;
        ValidationErrors = validationErrors;
    }

    public static Result<T> Success(T value) => new(true, string.Empty, value, 200);
    public static Result<T> Created(T value) => new(true, string.Empty, value, 201);
    public static Result<T> Failure(string error, int statusCode = 500) => new(false, error, default, statusCode);
    public static Result<T> Invalid(Dictionary<string, string[]> errors) =>
        new(false, "Validation Failed", default, 400, errors);
    public static Result<T> UnAuthorized(string error) => new(false, error, default, 401);
    public static Result<T> Conflict(string error) => new(false, error, default, 409);
}