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

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Register HttpClient for server-side components (base address can be overridden via configuration "ApiBaseUrl")
            var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:44369/";
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBase)
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
