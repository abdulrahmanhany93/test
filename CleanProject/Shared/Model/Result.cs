namespace CleanProject.Shared.Model;

public class Result<T>
{
    private Result(bool isSuccess, int statusCode, T? value, string? error)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public T? Value { get; private set; }
    public string? Error { get; private set; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, 200, value, null);
    }

    public static Result<T> Failure(string? error, int statusCode)
    {
        return new Result<T>(false, statusCode, default, error);
    }
}