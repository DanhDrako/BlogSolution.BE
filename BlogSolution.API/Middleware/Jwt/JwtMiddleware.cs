using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.API.Middleware.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserContextToRequest(context, token);
            }

            await _next(context);
        }

        private void AttachUserContextToRequest(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var claims = new ClaimsIdentity(jwtToken.Claims);

            // Add custom claims if needed
            // claims.AddClaim(new Claim("CustomClaim", "CustomValue"));

            context.Items["UserContext"] = claims;
        }
    }
}
