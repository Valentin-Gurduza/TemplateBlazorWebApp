using BlazorWebAppUTM.CQRS.Abstractions;
using BlazorWebAppUTM.CQRS.Users.Commands;
using BlazorWebAppUTM.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppUTM.CQRS.Users.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created successfully with ID: {UserId}, Name: {FirstName} {LastName}",
                    user.Id, user.FirstName, user.LastName);
                return new CreateUserResult(true, user.Id, null);
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogWarning("Failed to create user: {Errors}", errors);
            return new CreateUserResult(false, null, errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user with email: {Email}", request.Email);
            return new CreateUserResult(false, null, "An unexpected error occurred");
        }
    }
}