using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameReviewApi.Middleware.CustomAuthorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;
        public AuthorizationAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserDto)context.HttpContext.Items["User"];

            if (user == null || (_roles.Any() && !_roles.Contains(user.Role)))
            {
                context.Result = new JsonResult(new { message = "Неавторизованный" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
