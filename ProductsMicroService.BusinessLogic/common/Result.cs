namespace ProductsMicroService.BusinessLogic.common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public T? Value { get; }
    public int StatusCode { get; }

    private Result(bool isSuccess, string error, T? value, int statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value) => new(true, string.Empty, value, 200);
    public static Result<T> Created(T value) => new(true, string.Empty, value, 201);
    public static Result<T> Failure(string error, int statusCode = 500) => new(false, error, default, statusCode);
    public static Result<T> Invalid(string error) => new(false, error, default, 400);
    public static Result<T> UnAuthorized(string error) => new(false, error, default, 401);
    public static Result<T> Conflict(string error) => new(false, error, default, 409);
}
