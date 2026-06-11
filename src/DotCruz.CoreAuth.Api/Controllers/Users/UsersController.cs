using DotCruz.CoreAuth.Api.Controllers.Base;
using DotCruz.CoreAuth.Application.Commands.Users.ChangePassword;
using DotCruz.CoreAuth.Application.Commands.Users.CreateUser;
using DotCruz.CoreAuth.Application.Commands.Users.DeactivateUser;
using DotCruz.CoreAuth.Application.Commands.Users.UpdateUser;
using DotCruz.CoreAuth.Application.DTOs.Base;
using DotCruz.CoreAuth.Application.Queries;
using DotCruz.CoreAuth.Application.Queries.Users;
using DotCruz.CoreAuth.Application.Queries.Users.GetUserById;
using DotCruz.CoreAuth.Application.Queries.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotCruz.CoreAuth.Api.Controllers.Users;

public class UsersController(IMediator mediator) : DotCruzCoreAuthBaseController(mediator)
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    [Route("{Id}")]
    [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid Id)
    {
        var query = new GetUserByIdQuery(Id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<ResponseUserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetUsersQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(Id, request);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [Route("{Id}/change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromRoute] Guid Id, [FromBody] ChangePasswordRequest request)
    {
        var command = new ChangePasswordCommand(Id, request.CurrentPassword, request.NewPassword);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid Id)
    {
        var command = new DeactivateUserCommand(Id);
        await _mediator.Send(command);
        return NoContent();
    }
}
