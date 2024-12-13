using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class RoleIdAuthorizationRequirement : IAuthorizationRequirement
{
    public int RoleId { get; }

    public RoleIdAuthorizationRequirement(int roleId)
    {
        RoleId = roleId;
    }
}

public class RoleIdAuthorizationHandler : AuthorizationHandler<RoleIdAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleIdAuthorizationRequirement requirement)
    {
        var roleIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;

        if (roleIdClaim != null && int.Parse(roleIdClaim) == requirement.RoleId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
