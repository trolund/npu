namespace NPU.Infrastructure.Dtos;

public record PaginatedResponse<T>(
    IEnumerable<T>? Items,
    int TotalCount,
    int PageNumber,
    int PageSize,
    int NumberOfPages
);
