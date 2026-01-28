using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TODO_APP.Application.Interface;
using TODO_APP.Application.Service;
using TODO_APP.Application.Mapping;
using AutoMapper;

namespace TODO_APP.Application;

public static class DependencyInjectionApp
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // 1. Đăng ký các Service xử lý logic
        services.AddScoped<ITodoService, TodoService>();

      
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

    

        return services;
    }
}