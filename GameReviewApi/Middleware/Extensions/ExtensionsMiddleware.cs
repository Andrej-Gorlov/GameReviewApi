using GameReviewApi.Middleware.CustomAuthorization;
using GameReviewApi.Middleware.CustomException;

namespace GameReviewApi.Middleware.Extensions
{
    public static class ExtensionsMiddleware
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ErrorHandlerMiddleware>();
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<AuthorizationMiddleware>();
    }
}
