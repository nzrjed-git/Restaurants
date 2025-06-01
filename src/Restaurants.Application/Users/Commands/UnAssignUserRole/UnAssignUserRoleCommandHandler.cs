using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole
{
    internal class UnAssignUserRoleCommandHandler(
        ILogger<UnAssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand>
    {
        public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UnAssigning user role: {@Request}", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail);
            if (user == null) throw new NotFoundNameException(nameof(User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName);
            if (role == null) throw new NotFoundNameException(nameof(IdentityRole), request.RoleName);

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
