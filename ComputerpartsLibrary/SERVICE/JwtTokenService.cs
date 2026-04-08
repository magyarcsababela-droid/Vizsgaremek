using System;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using ComputerpartsLibrary.MODEL;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace ComputerpartsLibrary.SERVICE
{
    /// <summary>
    /// JWT (JSON Web Token) token generation and validation service
    /// </summary>
    public class JwtTokenService
    {
        private readonly string? _secretKey;
        private readonly string? _issuer;
        private readonly string? _audience;
        private readonly int _expireMinutes;
        
        /// <summary>
        /// Generates a JWT token for the given user
        /// </summary>
        public JwtTokenService(IConfiguration config)
        {
            // Read settings from configuration (appsettings.json)
            var section = config.GetSection("Jwt");
            _secretKey = section["Key"];
            _issuer = section["Issuer"];
            _audience = section["Audience"];

            if (!int.TryParse(section["ExpireMinutes"], out _expireMinutes))
            {
                // fallback to 60 minutes
                _expireMinutes = 60;
            }
        }

        // Generate token using full user object so we can include all user data as claims
        public JwtToken GenerateToken(ComputerpartsLibrary.MODEL.Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role ?? string.Empty),
                new Claim("authEmail", user.email ?? string.Empty),
                new Claim("authUsername", user.username ?? string.Empty),
                new Claim("authUserId", user.id.ToString()),
                // store created_at in ISO 8601 format
                new Claim("authCreatedAt", user.created_at.ToString("o"))
            };

            var expires = DateTime.UtcNow.AddMinutes(_expireMinutes);

            var securityKey = CreateSymmetricKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(jwt);

            return new JwtToken
            {
                Token = tokenString,
                Expiration = new DateTimeOffset(expires),
                UserId = user.id,
                Role = user.role ?? string.Empty
            };
        }
        
        /// <summary>
        /// Validates a JWT token and returns the user claims
        /// </summary>
        public ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = CreateSymmetricKey(),
                    ValidateIssuer = !string.IsNullOrEmpty(_issuer),
                    ValidIssuer = _issuer,
                    ValidateAudience = !string.IsNullOrEmpty(_audience),
                    ValidAudience = _audience,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = jwtHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException($"Token validation failed: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Creates a JWT token string
        /// </summary>
        private string CreateJwtTokenString(Claim[] claims)
        {
            var header = CreateHeader();
            var payload = CreatePayload(claims);
            var signature = CreateSignature(header, payload);
            
            return $"{header}.{payload}.{signature}";
        }
        
        /// <summary>
        /// Creates the JWT header
        /// </summary>
        private string CreateHeader()
        {
            // JsonSerializer is a static class - call Serialize() directly
            var headerJson = System.Text.Json.JsonSerializer.Serialize(new { alg = "HS256", typ = "JWT" });
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(headerJson));
        }
        
        /// <summary>
        /// Creates the JWT payload
        /// </summary>
        private string CreatePayload(Claim[] claims)
        {
            var payload = new System.Collections.Generic.Dictionary<string, object>();
            foreach (var claim in claims)
            {
                payload[claim.Type] = claim.Value;
            }
            
            // JsonSerializer is a static class - call Serialize() directly
            var payloadJson = System.Text.Json.JsonSerializer.Serialize(payload);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(payloadJson));
        }
        
        /// <summary>
        /// Creates the JWT signature
        /// </summary>
        private string CreateSignature(string header, string payload)
        {
            var message = $"{header}.{payload}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return Convert.ToBase64String(hash);
        }
        
        /// <summary>
        /// Creates a symmetric key for signing
        /// </summary>
        private SymmetricSecurityKey CreateSymmetricKey()
        {
            // Derive a 256-bit key from the secret using SHA256 so it is stable across restarts
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(_secretKey));
            return new SymmetricSecurityKey(hash);
        }
    }
}
