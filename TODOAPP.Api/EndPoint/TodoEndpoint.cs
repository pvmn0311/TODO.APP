using Microsoft.EntityFrameworkCore;
using TODO_APP.Api.Abstractions;
using TODO_APP.Application.Interface;
using TODO_APP.Application.Service;
using TODO_APP.Domain;
using TODO_APP.Infrastructure;
namespace TODO_APP.Api.EndPoint
{
    public class TodoEndpoint : IEndpoint 
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            //TODO
                var group = app.MapGroup("/todo");
                group.MapGet("", async (ITodoService todoService) =>
                {
                    var todos = await todoService.GetAllAsync();
                    return Results.Ok(todos);
                })
                .RequireAuthorization();

                group.MapGet("/{id:int}", async (int id, ITodoService todoService) =>
                {
                    var todo = await todoService.GetByIdAsync(id);
                    return todo is not null ? Results.Ok(todo) : Results.NotFound();
                })
                 .RequireAuthorization();

                group.MapPost("/create", async (TodoItem item, ITodoService todoService) =>
                {
                    await todoService.AddAsync(item);
                    return Results.Created($"/todo/create/{item.Id}", item);
                })
                 .RequireAuthorization();

                group.MapPut("/update/{id}", async (int id, TodoItem item, ITodoService todoService) =>
                {
                    if (id != item.Id) return Results.BadRequest();
                    await todoService.UpdateAsync(item);
                    return Results.NoContent();
                })
                 .RequireAuthorization();

                group.MapDelete("/delete/{id}", async (int id, ITodoService todoService) =>
                {
                    await todoService.DeleteAsync(id);
                    return Results.NoContent();
                })
                 .RequireAuthorization();
            //USER-ADMIN
            var adgr = app.MapGroup("/admin");

            adgr.MapGet("/getAllUsers", async (TodoDbContext db) =>
            {
                return Results.Ok(await db.Users.Select(u => new {u.Id, u.Username, u.Role}).ToListAsync());
            })
            .RequireAuthorization(policy => policy.RequireRole("Admin"));

            }
        }
    
}
