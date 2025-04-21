namespace Service.Utils.Models;

public record ApiResponse<T>(
  bool Success,
  T? Data = default,
  params string[] Errors
);
