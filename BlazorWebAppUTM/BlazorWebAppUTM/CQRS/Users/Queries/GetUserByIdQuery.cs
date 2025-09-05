using BlazorWebAppUTM.CQRS.Abstractions;

namespace BlazorWebAppUTM.CQRS.Users.Queries;

public record GetUserByIdQuery(string UserId) : IQuery<UserDto?>;

public record UserDto(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    DateTime CreatedAt
);