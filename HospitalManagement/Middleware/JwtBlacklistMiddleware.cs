using HospitalManagement.Services;

namespace HospitalManagement.Middleware
{
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TokenBlacklistService blacklist)
        {
            var auth = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(auth) && auth.StartsWith("Bearer "))
            {
                var token = auth.Replace("Bearer ", "");

                // Correct method name
                if (await blacklist.IsTokenBlacklisted(token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is blacklisted");
                    return;
                }
            }

            await _next(context);
        }
    }
}
