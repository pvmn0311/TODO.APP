using Microsoft.EntityFrameworkCore;
using TODO_APP.Application.Interface;
using TODO_APP.Domain;

namespace TODO_APP.Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoRepository (TodoDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }
        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }
        public async Task AddAsync(TodoItem item)
        {
            await _context.Todos.AddAsync(item);
            await _context.SaveChangesAsync(); 
        }
        public async Task UpdateAsync(TodoItem item)
        {
            _context.Todos.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.Todos.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
