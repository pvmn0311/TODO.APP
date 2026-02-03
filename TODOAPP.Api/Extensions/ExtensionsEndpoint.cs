using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TODO_APP.Api.Abstractions;
namespace TODO_APP.Api.Extensions
{
    public static class ExtensionsEndpoint
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoitTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IEndpoint)));
            foreach (var type in endpoitTypes) 
            {
                var instance = (IEndpoint)Activator.CreateInstance(type)!;
                instance.MapEndpoint(app);
            }
        }

    }
}
