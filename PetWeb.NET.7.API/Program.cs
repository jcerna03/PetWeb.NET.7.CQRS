using PetWeb.NET._7.Application;
using PetWeb.NET._7.Domain.Pets;
using PetWeb.NET._7.Infrastructure.Factories;
using PetWeb.NET._7.Infrastructure.Options;
using PetWeb.NET._7.Infrastructure.Repositories;

namespace PetWeb.NET._7.API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Configuring the options for database
        builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Name));

        // Add services to the container.
        builder.Services.AddScoped<IConnectionFactory, SQLConnectionFactory>();
        builder.Services.AddScoped<IPetRepository, PetRepository>();

        // Adding MediatR
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

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