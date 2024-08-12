using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.API.Controllers.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidationParameters _tokenValidationParams;

        public ValidationMiddleware(RequestDelegate next, TokenValidationParameters tokenValidationParams)
        {
            _next = next;
            _tokenValidationParams = tokenValidationParams;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);

                    if (validatedToken is JwtSecurityToken jwtSecurityToken)
                    {
                        var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                        if (!result)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsJsonAsync(new BaseResponseModel(StatusCodes.Status401Unauthorized, ResponseCodeConstants.TOKEN_INVALID, "Token is invalid."));
                            return;
                        }

                        var expClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp)?.Value;
                        if (expClaim != null && long.TryParse(expClaim, out var exp))
                        {
                            var expDateTime = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                            if (expDateTime < DateTime.UtcNow)
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsJsonAsync(new BaseResponseModel(StatusCodes.Status401Unauthorized, ResponseCodeConstants.TOKEN_EXPIRED, "Token has expired."));
                                return;
                            }
                        }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new BaseResponseModel(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, $"Token validation failed. Error: {ex.Message}"));
                    return;
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new BaseResponseModel(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, $"An error occurred while processing the token. Error: {ex.Message}"));
                    return;
                }
            }

            await _next(context);
        }
    }
}
