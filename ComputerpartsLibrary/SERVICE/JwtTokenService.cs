using System;
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
        private readonly string _secretKey;
        private const int ExpirationHours = 24; // Token érvényességi idő (24 óra)
        
        /// <summary>
        /// Generates a JWT token for the given user
        /// </summary>
        public JwtTokenService()
        {
            _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "ComputerPartsApp_SecretKey_2024!";
        }

        public JwtToken GenerateToken(int userId, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
            };

            var expires = DateTime.UtcNow.AddHours(ExpirationHours);

            var securityKey = CreateSymmetricKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: null,
                audience: null,
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
                UserId = userId,
                Role = role
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
                    ValidateIssuer = false,
                    ValidateAudience = false, // Audience validation can be added later
                    ClockSkew = TimeSpan.Zero
                };
                
                var validatedToken = jwtHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                
                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    // Convert Claims to ClaimsIdentity for ClaimsPrincipal constructor
                    var claimsIdentities = new List<ClaimsIdentity>();
                    foreach (var claim in jwtSecurityToken.Claims)
                    {
                        var identity = new ClaimsIdentity();
                        identity.AddClaim(claim);
                        claimsIdentities.Add(identity);
                    }
                    return new ClaimsPrincipal(claimsIdentities);
                }
                
                throw new InvalidOperationException("Invalid token type");
            }
            catch (Exception ex)
            {
                // Token validation failed - could be expired, invalid signature, etc.
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
