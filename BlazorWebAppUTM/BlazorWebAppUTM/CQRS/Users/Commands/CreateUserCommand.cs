using BlazorWebAppUTM.CQRS.Abstractions;

namespace BlazorWebAppUTM.CQRS.Users.Commands;

public record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
) : ICommand<CreateUserResult>;

public record CreateUserResult(
    bool Success,
    string? UserId,
    string? ErrorMessage
);