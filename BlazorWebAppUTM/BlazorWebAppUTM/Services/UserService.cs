using BlazorWebAppUTM.CQRS.Users.Commands;
using BlazorWebAppUTM.CQRS.Users.Queries;
using MediatR;

namespace BlazorWebAppUTM.Services;

public interface IUserService
{
    Task<CreateUserResult> CreateUserAsync(CreateUserCommand command);
    Task<UserDto?> GetUserByIdAsync(string userId);
}

public class UserService : IUserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CreateUserResult> CreateUserAsync(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto?> GetUserByIdAsync(string userId)
    {
        return await _mediator.Send(new GetUserByIdQuery(userId));
    }
}