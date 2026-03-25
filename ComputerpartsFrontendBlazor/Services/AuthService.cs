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

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Use the window wrapper functions defined in App.razor to access browser localStorage
                var token = await _js.InvokeAsync<string>("getLocalToken");
                if (!string.IsNullOrEmpty(token))
                {
                    Token = token;
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    IsAuthenticated = true;
                }
            }
            catch
            {
                // ignore
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
                // user is returned as arbitrary object; try to parse username if present
                if (result.user is JsonElement je && je.ValueKind == JsonValueKind.Object && je.TryGetProperty("username", out var uname))
                {
                    Username = uname.GetString();
                }
                IsAuthenticated = true;

                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                await _js.InvokeVoidAsync("setLocalToken", Token);
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
            IsAuthenticated = false;
            _http.DefaultRequestHeaders.Authorization = null;
            try { await _js.InvokeVoidAsync("removeLocalToken"); } catch { }
        }

        private class LoginResponse
        {
            public object? user { get; set; }
            public string? token { get; set; }
            public DateTimeOffset? expires { get; set; }
        }
    }
}
