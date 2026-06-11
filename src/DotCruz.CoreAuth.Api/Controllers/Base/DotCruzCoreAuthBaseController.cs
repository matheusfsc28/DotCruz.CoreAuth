using DotCruz.CoreAuth.Application.DTOs.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotCruz.CoreAuth.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
public class DotCruzCoreAuthBaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public DotCruzCoreAuthBaseController(IMediator mediator)
        => _mediator = mediator;
}
