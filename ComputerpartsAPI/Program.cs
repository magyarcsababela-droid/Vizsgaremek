using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using Microsoft.EntityFrameworkCore;
using ComputerpartsLibrary.SERVICE;

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
            builder.Services.AddScoped<IPrebuiltPcCompService, PrebuiltPcCompService>();
            builder.Services.AddScoped<IPrebuiltPcService, PrebuiltPcService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
