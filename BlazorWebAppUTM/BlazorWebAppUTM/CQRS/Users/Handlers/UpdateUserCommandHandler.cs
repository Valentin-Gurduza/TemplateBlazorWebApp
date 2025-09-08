using BlazorWebAppUTM.CQRS.Abstractions;
using BlazorWebAppUTM.CQRS.Users.Commands;
using BlazorWebAppUTM.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppUTM.CQRS.Users.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            if (user == null)
            {
                _logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                return new UpdateUserResult(false, "User not found");
            }

            // Update user properties
            user.Email = request.Email;
            user.UserName = request.Email; // Keep UserName in sync with Email
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User updated successfully with ID: {UserId}, Name: {FirstName} {LastName}",
                    user.Id, user.FirstName, user.LastName);
                return new UpdateUserResult(true, null);
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogWarning("Failed to update user: {Errors}", errors);
            return new UpdateUserResult(false, errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user with ID: {UserId}", request.UserId);
            return new UpdateUserResult(false, "An unexpected error occurred");
        }
    }
}