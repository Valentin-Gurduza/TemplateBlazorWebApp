using BlazorWebAppUTM.CQRS.Abstractions;

namespace BlazorWebAppUTM.CQRS.Users.Commands;

public record UpdateUserCommand(
    string UserId,
    string Email,
    string FirstName,
    string LastName
) : ICommand<UpdateUserResult>;

public record UpdateUserResult(
    bool Success,
    string? ErrorMessage
);