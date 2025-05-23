namespace WebAPI.Common;

public sealed class ApiResponse<TResponse>
{
	public ApiResponse(
		TResponse? data, string message = "", bool success = true)
	{
		Data = data;
		Message = message;
		Success = success;
	}

	public TResponse? Data { get; }
	public string Message { get; }
	public bool Success { get; }

	public static ApiResponse<TResponse> Ok(TResponse? data = default!, string message = "Success!")
		=> new ApiResponse<TResponse>(
			data: data,
			message: message);

	public static ApiResponse<TResponse> Fail(string message)
		=> new ApiResponse<TResponse>(
			data: default!,
			message: message,
			success: false);
}
