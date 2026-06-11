using MediatR;
using System;

namespace DotCruz.CoreAuth.Application.Queries.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<ResponseUserDto>;
