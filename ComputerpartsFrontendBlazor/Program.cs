using System;
using System.Net.Http;
using ComputerpartsFrontendBlazor.Components;

namespace ComputerpartsFrontendBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Szolgáltatások hozzáadása a konténerhez.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // HttpClient regisztrálása a szerver oldali komponensekhez (az alap cím felülírható a konfigurációban az "ApiBaseUrl"-lel)
            var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:44369/";
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBase)
            });

            // Hitelesítési szolgáltatás hozzáadása a token tárolásához
            builder.Services.AddScoped<ComputerpartsFrontendBlazor.Services.AuthService>();

            var app = builder.Build();

            // HTTP kérés-feldolgozó csővezeték konfigurálása.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
