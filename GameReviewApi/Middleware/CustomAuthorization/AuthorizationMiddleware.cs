using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Middleware.CustomAuthorization
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IHelperToken helperToken, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = helperToken.ValidateToken(token);
                // прикрепляется пользователь к контексту при успешной проверке jwt.
                context.Items["User"] = await userService.GetByIdAsyncService(userId.Value);
            }
            await _next(context);
        }
    }
}
