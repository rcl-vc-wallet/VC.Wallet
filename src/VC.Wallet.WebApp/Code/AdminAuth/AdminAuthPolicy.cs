using Microsoft.AspNetCore.Authorization;

namespace VC.Wallet.WebApp
{
    public class AdminAuthHandler : AuthorizationHandler<AdminAuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAuthRequirement requirement)
        {
            bool b = AdminService.IsAdmin(context.User);

            if(b == false)
            {
                return Task.CompletedTask;
            }

            if (b == true)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class AdminAuthRequirement : IAuthorizationRequirement
    { 
    }
}
