using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using TODO_APP.Application.DTOs;
using TODO_APP.Application.Interface;
using TODO_APP.Application.Service;
using TODO_APP.Domain;

namespace TODO_APP.Application.Service
{
    public class TodoService : ITodoService
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;
        public TodoService (ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            var item = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TodoDto>>(item);
            
            return dtos;
        }
        public async Task<TodoDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return _mapper.Map<TodoDto>(item);
        }
        public async Task AddAsync(TodoItem item)
        {
            await _repository.AddAsync(item);
        }
        public async Task UpdateAsync(TodoItem item)
        {
            await _repository.UpdateAsync(item);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
