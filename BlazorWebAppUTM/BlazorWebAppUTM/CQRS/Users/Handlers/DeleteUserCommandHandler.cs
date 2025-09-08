using BlazorWebAppUTM.CQRS.Abstractions;
using BlazorWebAppUTM.CQRS.Users.Commands;
using BlazorWebAppUTM.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppUTM.CQRS.Users.Handlers;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, DeleteUserResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            if (user == null)
            {
                _logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                return new DeleteUserResult(false, "User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted successfully with ID: {UserId}, Email: {Email}",
                    user.Id, user.Email);
                return new DeleteUserResult(true, null);
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogWarning("Failed to delete user: {Errors}", errors);
            return new DeleteUserResult(false, errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user with ID: {UserId}", request.UserId);
            return new DeleteUserResult(false, "An unexpected error occurred");
        }
    }
}