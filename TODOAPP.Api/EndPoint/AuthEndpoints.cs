using Microsoft.EntityFrameworkCore;
using TODO_APP.Api.Helpers;
using TODO_APP.Infrastructure;
using TODO_APP.Domain; // Đảm bảo có namespace của class User
using BCrypt.Net;
using System.Threading.Tasks;

namespace TODO_APP.Api.EndPoint;

public static class AuthEndpoints
{
    public static async Task MapAuthEndpoints(this IEndpointRouteBuilder app, IConfiguration config)
    {
        var gr = app.MapGroup("/account");



        gr.MapPost("/register", async (RegisterRequest request, TodoDbContext db)=>
        {
            if (await db.Users.AnyAsync(u => u.Username == request.Username))
                return Results.BadRequest("Username da ton tai.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Role = "User"
            };
            db.Users.Add(newUser);
            await db.SaveChangesAsync();
            return Results.Ok("Dang ky tai khoan nguoi dung thanh cong!");
        });
        gr.MapPost("/login", async (LoginRequest login, TodoDbContext db, IConfiguration config) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                return Results.Unauthorized();
            }
            var token = TokenGenerator.GenerateJwtToken(
                config["Jwt:Key"]!,
                config["Jwt:Issuer"]!,
                config["Jwt:Audience"]!,
                user.Username,
                user.Role,
                user.Id
                );
            return Results.Ok(new { token });
        });
    }

    public record LoginRequest(string Username, string Password);
    public record RegisterRequest(string Username, string Password);
}