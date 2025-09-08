using BlazorWebAppUTM.CQRS.Abstractions;

namespace BlazorWebAppUTM.CQRS.Users.Commands;

public record DeleteUserCommand(string UserId) : ICommand<DeleteUserResult>;

public record DeleteUserResult(
    bool Success,
    string? ErrorMessage
);