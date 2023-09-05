using Tienda.Application.Authorization;

namespace Tienda.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var user = jwtUtils.ValidateJwtToken(token);

            if (user != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = user;
            }

            await _next(context);
        }
    }
}
