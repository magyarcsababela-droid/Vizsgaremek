using System;

namespace ComputerpartsLibrary.MODEL
{
    /// <summary>
    /// JWT Token model for authentication
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// The JWT token string
        /// </summary>
        public string Token { get; set; } = null!;
        
        /// <summary>
        /// Token expiration time
        /// </summary>
        public DateTimeOffset Expiration { get; set; }
        
        /// <summary>
        /// User ID associated with the token
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// User role (e.g., "Admin", "User")
        /// </summary>
        public string Role { get; set; } = null!;
    }
}
