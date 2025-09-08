using BlazorWebAppUTM.CQRS.Abstractions;
using BlazorWebAppUTM.CQRS.Users.Queries;
using BlazorWebAppUTM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppUTM.CQRS.Users.Handlers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GetUsersQueryHandler> _logger;

    public GetUsersQueryHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<GetUsersQueryHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<GetUsersResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _userManager.Users.AsQueryable();
            
            var totalCount = await query.CountAsync(cancellationToken);
            
            var users = await query
                .OrderByDescending(u => u.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserDto(
                    u.Id,
                    u.Email!,
                    u.FirstName ?? "Not specified",
                    u.LastName ?? "Not specified",
                    u.CreatedAt
                ))
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} users (page {PageNumber} of size {PageSize})", 
                users.Count, request.PageNumber, request.PageSize);

            return new GetUsersResult(users, totalCount, request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users for page {PageNumber}", request.PageNumber);
            return new GetUsersResult(Enumerable.Empty<UserDto>(), 0, request.PageNumber, request.PageSize);
        }
    }
}