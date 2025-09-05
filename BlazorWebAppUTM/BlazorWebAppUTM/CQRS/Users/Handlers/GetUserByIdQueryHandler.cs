using BlazorWebAppUTM.CQRS.Abstractions;
using BlazorWebAppUTM.CQRS.Users.Queries;
using BlazorWebAppUTM.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppUTM.CQRS.Users.Handlers;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByIdQueryHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<GetUserByIdQueryHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            if (user == null)
            {
                _logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                return null;
            }

            return new UserDto(
                user.Id,
                user.Email!,
                user.FirstName ?? "Not specified",
                user.LastName ?? "Not specified",
                user.CreatedAt
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user with ID: {UserId}", request.UserId);
            return null;
        }
    }
}