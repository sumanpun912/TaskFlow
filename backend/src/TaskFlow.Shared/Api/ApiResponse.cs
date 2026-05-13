namespace TaskFlow.Shared.Api;

public sealed class ApiResponse<T>
{
    public T? Data {get; init;}
    public List<string>? Errors {get; init;} = [];
    public bool Success {get; init;}

    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T>
        {
            Data = data,
            Errors = [],
            Success = true
        };
    }

    public static ApiResponse<T> Fail(params string[] errors)
    {
        return new ApiResponse<T>
        {
            Data = default,
            Errors = errors.ToList(),
            Success = false
        };
    }
}