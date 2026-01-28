using Microsoft.EntityFrameworkCore;
using TODO_APP.Domain;
namespace TODO_APP.Infrastructure
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
        public DbSet<TodoItem> Todos { get; set; }
    }
}
