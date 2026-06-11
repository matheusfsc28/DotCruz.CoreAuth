namespace DotCruz.CoreAuth.Application.DTOs.Base;

public class ErrorResponseDto
{
    public IEnumerable<string> Errors { get; private set; }

    public ErrorResponseDto(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public ErrorResponseDto(string error)
    {
        Errors = [error];
    }
}
