using TODO_APP.Api.Abstractions;
using TODO_APP.Application.Interface;
using TODO_APP.Application.Service;
using TODO_APP.Domain;
namespace TODO_APP.Api.EndPoint
{
    public class TodoEndpoint : IEndpoint 
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            
                var group = app.MapGroup("/todo");
                group.MapGet("", async (ITodoService todoService) =>
                {
                    var todos = await todoService.GetAllAsync();
                    return Results.Ok(todos);
                });

                group.MapGet("/{id}", async (int id, ITodoService todoService) =>
                {
                    var todo = await todoService.GetByIdAsync(id);
                    return todo is not null ? Results.Ok(todo) : Results.NotFound();
                });

                group.MapPost("/create", async (TodoItem item, ITodoService todoService) =>
                {
                    await todoService.AddAsync(item);
                    return Results.Created($"/todo/create/{item.Id}", item);
                });

                group.MapPut("/update/{id}", async (int id, TodoItem item, ITodoService todoService) =>
                {
                    if (id != item.Id) return Results.BadRequest();
                    await todoService.UpdateAsync(item);
                    return Results.NoContent();
                });

                group.MapDelete("/delete/{id}", async (int id, ITodoService todoService) =>
                {
                    await todoService.DeleteAsync(id);
                    return Results.NoContent();
                });
            }
        }
    
}
