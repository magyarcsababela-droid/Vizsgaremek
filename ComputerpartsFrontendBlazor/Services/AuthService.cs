using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Text.Json;

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
                    if (!string.IsNullOrEmpty(user.username)) Username = user.username;
                    if (!string.IsNullOrEmpty(user.email)) Email = user.email;
                    if (!string.IsNullOrEmpty(user.role)) Role = user.role;
                    if (user.id != 0) UserId = user.id;
                    // persist to localStorage
                    try { if (!string.IsNullOrEmpty(Username)) await _js.InvokeVoidAsync("setLocalUsername", Username); } catch { }
                    try { if (!string.IsNullOrEmpty(Email)) await _js.InvokeVoidAsync("setLocalEmail", Email); } catch { }
                    try { if (UserId.HasValue) await _js.InvokeVoidAsync("setLocalUserId", UserId.Value.ToString()); } catch { }
                    try { if (!string.IsNullOrEmpty(Role)) await _js.InvokeVoidAsync("setLocalRole", Role); } catch { }
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
                var username = await _js.InvokeAsync<string>("getLocalUsername");

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
                }
                catch
                {
                    // invalid token format -> logout
                    await LogoutAsync();
                    return;
                }

                // token looks unexpired locally, set it and verify against server
                Token = token;
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                IsAuthenticated = true;
                if (!string.IsNullOrEmpty(username))
                {
                    Username = username;
                }

                try
                {
                    var email = await _js.InvokeAsync<string>("getLocalEmail");
                    if (!string.IsNullOrEmpty(email)) Email = email;
                    var uid = await _js.InvokeAsync<string>("getLocalUserId");
                    var role = await _js.InvokeAsync<string>("getLocalRole");
                    if (!string.IsNullOrEmpty(role)) Role = role;
                    if (int.TryParse(uid, out var parsedUid)) UserId = parsedUid;
                }
                catch { }

                // Verify server will accept the token: request current user
                try
                {
                    var resp = await _http.GetAsync("api/auth/me");
                    if (!resp.IsSuccessStatusCode)
                    {
                        // server rejected token (expired/invalid) -> logout
                        await LogoutAsync();
                        return;
                    }
                    // success -> update current user info
                    try
                    {
                        var user = await resp.Content.ReadFromJsonAsync<ComputerpartsLibrary.MODEL.Users>();
                        if (user != null)
                        {
                            if (!string.IsNullOrEmpty(user.username)) Username = user.username;
                            if (!string.IsNullOrEmpty(user.email)) Email = user.email;
                            if (!string.IsNullOrEmpty(user.role)) Role = user.role;
                            if (user.id != 0) UserId = user.id;
                            try { if (!string.IsNullOrEmpty(Username)) await _js.InvokeVoidAsync("setLocalUsername", Username); } catch { }
                            try { if (!string.IsNullOrEmpty(Email)) await _js.InvokeVoidAsync("setLocalEmail", Email); } catch { }
                            try { if (UserId.HasValue) await _js.InvokeVoidAsync("setLocalUserId", UserId.Value.ToString()); } catch { }
                            try { if (!string.IsNullOrEmpty(Role)) await _js.InvokeVoidAsync("setLocalRole", Role); } catch { }
                        }
                    }
                    catch { }
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

                // Try to extract username from the returned user object in a robust way
                try
                {
                    if (result.user is JsonElement je && je.ValueKind == JsonValueKind.Object)
                    {
                        // try to read numeric id if provided
                        if (je.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.Number)
                        {
                            if (idProp.TryGetInt32(out var parsedId))
                                UserId = parsedId;
                        }
                        if (je.TryGetProperty("username", out var unameProp) || je.TryGetProperty("Username", out unameProp) || je.TryGetProperty("userName", out unameProp))
                        {
                            Username = unameProp.GetString();
                        }
                        else
                        {
                            // fallback: try deserializing to Users model
                            try
                            {
                                var u = JsonSerializer.Deserialize<ComputerpartsLibrary.MODEL.Users>(je.GetRawText());
                                if (u != null && !string.IsNullOrEmpty(u.username))
                                    Username = u.username;
                            }
                            catch { }
                        }
                    }
                }
                catch { }

                // if server did not return a username, fall back to the login identifier
                if (string.IsNullOrEmpty(Username))
                {
                    Username = username; // fallback to what user entered (email or username)
                }

                IsAuthenticated = true;

                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                await _js.InvokeVoidAsync("setLocalToken", Token);
                try { var toSave = !string.IsNullOrEmpty(Username) ? Username : username; if (!string.IsNullOrEmpty(toSave)) await _js.InvokeVoidAsync("setLocalUsername", toSave); } catch { }
                try { if (UserId.HasValue) await _js.InvokeVoidAsync("setLocalUserId", UserId.Value.ToString()); } catch { }
                try { if (!string.IsNullOrEmpty(Role)) await _js.InvokeVoidAsync("setLocalRole", Role); } catch { }
                try
                {
                    // try to extract email from returned user object
                    if (result.user is JsonElement je && je.ValueKind == JsonValueKind.Object)
                    {
                        if (je.TryGetProperty("email", out var emailProp) || je.TryGetProperty("Email", out emailProp))
                        {
                            Email = emailProp.GetString();
                            if (!string.IsNullOrEmpty(Email)) await _js.InvokeVoidAsync("setLocalEmail", Email);
                        }
                    }
                }
                catch { }
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
    }
}
