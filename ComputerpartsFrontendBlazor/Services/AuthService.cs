using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Linq;
using System.Security.Claims;

namespace ComputerpartsFrontendBlazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public bool IsAuthenticated { get; private set; }
        public string? Token { get; private set; }
        public string? Username { get; private set; }
        public string? Email { get; private set; }
        public string? Role { get; private set; }
        public int? UserId { get; private set; }
        public event Action? AuthStateChanged;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        // Expose a PUT helper that includes the current token explicitly on the request
        public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T payload)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = JsonContent.Create(payload)
            };

            if (!string.IsNullOrEmpty(Token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            return await _http.SendAsync(req);
        }

        // Expose a DELETE helper that includes the current token explicitly on the request
        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
            if (!string.IsNullOrEmpty(Token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            return await _http.SendAsync(req);
        }

        // Expose a POST helper that includes the current token explicitly on the request
        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T payload)
        {
            // build request with explicit Authorization header to avoid relying on DefaultRequestHeaders
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(payload)
            };

            // prefer explicit Token, but fall back to HttpClient default header if present
            if (!string.IsNullOrEmpty(Token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            else if (_http.DefaultRequestHeaders.Authorization != null)
            {
                req.Headers.Authorization = _http.DefaultRequestHeaders.Authorization;
            }

            return await _http.SendAsync(req);
        }

        // GET helper that includes the current token explicitly on the request and deserializes JSON
        public async Task<T?> GetJsonAsync<T>(string url)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrEmpty(Token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            var resp = await _http.SendAsync(req);
            if (!resp.IsSuccessStatusCode) return default;
            try
            {
                var obj = await resp.Content.ReadFromJsonAsync<T>();
                return obj;
            }
            catch
            {
                return default;
            }
        }

        private async Task FetchCurrentUserAsync()
        {
            try
            {
                var resp = await _http.GetAsync("api/auth/me");
                if (!resp.IsSuccessStatusCode) return;
                var user = await resp.Content.ReadFromJsonAsync<ComputerpartsLibrary.MODEL.Users>();
                if (user != null)
                {
                    // prefer token claims as source of truth; update in-memory values from server only if missing
                    if (string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(user.username)) Username = user.username;
                    if (string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(user.email)) Email = user.email;
                    if (string.IsNullOrEmpty(Role) && !string.IsNullOrEmpty(user.role)) Role = user.role;
                    if (!UserId.HasValue && user.id != 0) UserId = user.id;
                }
            }
            catch { }
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Use the window wrapper functions defined in App.razor to access browser localStorage
                var token = await _js.InvokeAsync<string>("getLocalToken");

                if (string.IsNullOrEmpty(token))
                {
                    // No token in browser: ensure logged-out state
                    Token = null;
                    Username = null;
                    Role = null;
                    Email = null;
                    UserId = null;
                    IsAuthenticated = false;
                    try { _http.DefaultRequestHeaders.Authorization = null; } catch { }
                    AuthStateChanged?.Invoke();
                    return;
                }

                // Basic client-side validation: try to decode token and check expiry
                try
                {
                    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(token);
                    if (jwt.ValidTo < DateTime.UtcNow)
                    {
                        // token expired client-side -> force logout
                        await LogoutAsync();
                        return;
                    }

                    // token looks valid locally: set and extract claims as source of user info
                    Token = token;
                    PopulateFromToken(token);
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    IsAuthenticated = true;
                }
                catch
                {
                    // invalid token format -> logout
                    await LogoutAsync();
                    return;
                }

                // Verify server will accept the token: request current user
                // Optionally verify token on server; if server rejects, force logout
                try
                {
                    var resp = await _http.GetAsync("api/auth/me");
                    if (!resp.IsSuccessStatusCode)
                    {
                        await LogoutAsync();
                        return;
                    }
                }
                catch
                {
                    // network/server error - be conservative and logout
                    await LogoutAsync();
                    return;
                }

                // notify subscribers that auth state may have changed
                AuthStateChanged?.Invoke();
            }
            catch
            {
                // ignore unexpected errors
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var payload = new { Username = username, Password = password };
            try
            {
                var resp = await _http.PostAsJsonAsync("api/auth/login", payload);
                if (!resp.IsSuccessStatusCode) return false;

                var result = await resp.Content.ReadFromJsonAsync<LoginResponse>();
                if (result?.token == null) return false;

                Token = result.token;
                // populate user info from token claims (token contains all needed user fields)
                try
                {
                    PopulateFromToken(Token);
                }
                catch { }

                IsAuthenticated = true;
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                await _js.InvokeVoidAsync("setLocalToken", Token);
                try { await _js.InvokeVoidAsync("console.log", $"AuthService.LoginAsync: username={Username} token={(Token?.Length>10?Token.Substring(0,10)+"...":Token)}"); } catch { }
                AuthStateChanged?.Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            Token = null;
            Username = null;
            Role = null;
            Email = null;
            IsAuthenticated = false;
            _http.DefaultRequestHeaders.Authorization = null;
            try { await _js.InvokeVoidAsync("removeLocalToken"); } catch { }
            try { await _js.InvokeVoidAsync("removeLocalUsername"); } catch { }
            try { await _js.InvokeVoidAsync("removeLocalRole"); } catch { }
            try { await _js.InvokeVoidAsync("removeLocalEmail"); } catch { }
            AuthStateChanged?.Invoke();
        }

        private class LoginResponse
        {
            public object? user { get; set; }
            public string? token { get; set; }
            public DateTimeOffset? expires { get; set; }
        }

        // Parse token and populate Username, Email, Role and UserId from claims
        private void PopulateFromToken(string token)
        {
            try
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                Username = jwt.Claims.FirstOrDefault(c => c.Type == "authUsername" || c.Type == "username" || c.Type == ClaimTypes.Name)?.Value;
                Email = jwt.Claims.FirstOrDefault(c => c.Type == "authEmail" || c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
                Role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
                var idClaim = jwt.Claims.FirstOrDefault(c => c.Type == "authUserId" || c.Type == ClaimTypes.NameIdentifier || c.Type == "userId")?.Value;
                if (int.TryParse(idClaim, out var parsed)) UserId = parsed;

                // Do NOT persist individual user fields to localStorage — only the token should be stored.
            }
            catch
            {
                // ignore parse errors
            }
        }
    }
}
