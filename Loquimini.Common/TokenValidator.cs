using Loquimini.Common.Enums;
using Loquimini.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Loquimini.Common
{
    public class TokenValidator : ISecurityTokenValidator
    {
        private JwtSecurityTokenHandler _tokenHandler;

        public TokenValidator(IServiceProvider serviceProvider)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool CanValidateToken
        {
            get
            {
                return true;
            }
        }

        public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

        public bool CanReadToken(string securityToken)
        {
            return _tokenHandler.CanReadToken(securityToken);
        }

        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            ClaimsPrincipal claimsPrincipal = null;
            try
            {
                claimsPrincipal = _tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                throw new UnauthorizedException(UnauthorizeExceptionType.TokenExpired, "Token has been expired");
            }
            catch (Exception e)
            {
                throw new UnauthorizedException(UnauthorizeExceptionType.TokenInvalid, e.Message);
            }

            return claimsPrincipal;
        }
    }
}
