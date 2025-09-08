using BlazorWebAppUTM.CQRS.Abstractions;

namespace BlazorWebAppUTM.CQRS.Users.Queries;

public record GetUsersQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IQuery<GetUsersResult>;

public record GetUsersResult(
    IEnumerable<UserDto> Users,
    int TotalCount,
    int PageNumber,
    int PageSize
);

public record UserListDto(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    DateTime CreatedAt
);