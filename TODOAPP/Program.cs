using Microsoft.AspNetCore.Authentication.JwtBearer;
using TODO_APP.Api.EndPoint;
using TODO_APP.Api.Extensions;
using TODO_APP.Application;
using TODO_APP.Infrastructure;
using Serilog;
public class Program
{
    public static void Main(string[] args)
    {
        // Cấu hình Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Ghi log theo ngày
            .CreateLogger();

        try
        {
            Log.Information("Ứng dụng đang khởi động...");
            var builder = WebApplication.CreateBuilder(args);

            // Sử dụng Serilog thay cho Log mặc định
            builder.Host.UseSerilog();
        builder.Services.AddJwtAuthentication(builder.Configuration);


        // 1. Swagger - Cấu hình để hỗ trợ dán Token JWT
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Nhập Token của bạn theo định dạng: Bearer {token}"
            });
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] {}
                }
            });
        });

        // 2. Register Services từ các Layer
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructure(builder.Configuration);

        // 3. Đăng ký Authentication & Authorization Service
       
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy => policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader());
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<TODO_APP.Api.Middlewares.ExceptionMiddleware>();
        app.UseCors("AllowAngular");

        app.UseAuthentication(); 
        app.UseAuthorization();  

        app.MapAuthEndpoints(builder.Configuration);
        app.MapEndpoints();

        app.Run();
    }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Ứng dụng bị sập ngoài ý muốn!");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}