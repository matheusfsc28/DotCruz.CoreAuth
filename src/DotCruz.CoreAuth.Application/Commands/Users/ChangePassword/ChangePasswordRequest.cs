namespace DotCruz.CoreAuth.Application.Commands.Users.ChangePassword;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
