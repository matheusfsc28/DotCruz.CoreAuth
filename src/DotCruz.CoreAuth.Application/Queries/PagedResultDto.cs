using System.Collections.Generic;

namespace DotCruz.CoreAuth.Application.Queries;

public record PagedResultDto<T>(
    IEnumerable<T> Items,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages
);
