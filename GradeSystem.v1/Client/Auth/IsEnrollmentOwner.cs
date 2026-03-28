using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GradeSystem.v1.Client.Auth
{
    public class IsEnrollmentOwnerRequirement : IAuthorizationRequirement
    {
    }
    public class IsEnrollmentOwner : AuthorizationHandler<IsEnrollmentOwnerRequirement, Enrollment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsEnrollmentOwnerRequirement requirement, Enrollment enrollment)
        {
            if (context.User == null || enrollment == null)
            {
                return Task.CompletedTask;
            }
            var userIdClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Email);
            if (userIdClaim == null)
            {
                return Task.CompletedTask;
            }
            if (enrollment.Subject.Teacher.UserID.ToString() == userIdClaim.Value || context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
