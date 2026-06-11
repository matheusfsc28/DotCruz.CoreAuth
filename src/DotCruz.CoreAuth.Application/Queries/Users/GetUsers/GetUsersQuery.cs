using MediatR;

namespace DotCruz.CoreAuth.Application.Queries.Users.GetUsers;

public record GetUsersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PagedResultDto<ResponseUserDto>>;
