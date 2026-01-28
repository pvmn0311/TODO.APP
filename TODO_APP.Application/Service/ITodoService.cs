using AutoMapper;
using TODO_APP.Application.DTOs;
using TODO_APP.Application.Interface;
using TODO_APP.Application.Service;
using TODO_APP.Domain;

namespace TODO_APP.Application.Service
{
    public interface ITodoService
    {   
        Task<IEnumerable<TodoDto>> GetAllAsync();
        Task<TodoDto?> GetByIdAsync(int id);
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);
    }
}

