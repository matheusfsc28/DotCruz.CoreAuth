using DotCruz.CoreAuth.Api.Controllers.Base;
using DotCruz.CoreAuth.Application.Commands.Auth.ActivateAccount;
using DotCruz.CoreAuth.Application.Commands.Auth.Login;
using DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.RequestPasswordReset;
using DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.ResetPassword;
using DotCruz.CoreAuth.Application.Commands.Auth.RefreshTokens;
using DotCruz.CoreAuth.Application.Commands.Auth.RevokeAllUserTokens;
using DotCruz.CoreAuth.Application.DTOs.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotCruz.CoreAuth.Api.Controllers.Auth;

public class AuthController(IMediator mediator) : DotCruzCoreAuthBaseController(mediator)
{
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ResponseLoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [Route("activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Activate([FromBody] ActivateAccountCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType(typeof(ResponseTokensDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [Route("password-reset/request")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [Route("password-reset/reset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [Route("revoke-tokens/{UserId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevokeTokens([FromRoute] Guid UserId)
    {
        var command = new RevokeAllUserTokensCommand(UserId);
        await _mediator.Send(command);
        return NoContent();
    }
}
