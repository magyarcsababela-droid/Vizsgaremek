using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using ComputerpartsLibrary.SERVICE;
using ComputerpartsAPI.Controllers;

namespace ComputerpartsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ComputerpatsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IBuildComponentService, BuildComponentService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IComponentService, ComponentService>();
            builder.Services.AddScoped<IComponentTypeService, ComponentTypeService>();
            builder.Services.AddScoped<ICustomBuildService, CustomBuildService>();
            builder.Services.AddScoped<IInventoryComponentService, InventoryComponentService>();
            builder.Services.AddScoped<IInventoryProductService, InventoryProductService>();
            builder.Services.AddScoped<IOrderItemBService, OrderItemBService>();
            builder.Services.AddScoped<IOrderItemPService, OrderItemPService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<JwtTokenService>(); // Register JWT token service
            builder.Services.AddScoped<IPrebuiltPcCompService, PrebuiltPcCompService>();
            builder.Services.AddScoped<IPrebuiltPcService, PrebuiltPcService>();

            // Register authentication services and controller
            builder.Services.AddScoped<UserAuthService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            

            // Use JWT Secret Key from configuration
            var jwtSecret = builder.Configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new InvalidOperationException("JWT:SecretKey must be set in appsettings.json");
            }

            // Configure JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                // Use SHA256 of the secret to derive a stable 256-bit key (same as JwtTokenService)
                using var sha = System.Security.Cryptography.SHA256.Create();
                var keyHash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(jwtSecret));
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyHash),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            // Map attribute-routed controllers
            app.MapControllers();

            app.Run();
        }
    }
}
