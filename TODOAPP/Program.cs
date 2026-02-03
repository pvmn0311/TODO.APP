
using TODO_APP.Api.Extensions;
using TODO_APP.Infrastructure;
using TODO_APP.Application;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //AddCors

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy => policy.WithOrigins("http://locoalhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //Endpoint
        app.MapEndpoints();

        //app.UseRouting(0);
        //Angular
        app.UseCors("AllowAngular");
        // --- Minimal API Endpoints --
        app.Run();
    }
}