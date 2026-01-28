using AutoMapper;
using TODO_APP.Domain;
using TODO_APP.Application.DTOs;
namespace TODO_APP.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Ánh xạ từ Entity sang DTO (Dùng khi lấy dữ liệu ra)
        CreateMap<TodoItem, TodoDto>();

        // Ánh xạ từ DTO sang Entity (Dùng khi nhận dữ liệu từ Angular để lưu vào DB)
        CreateMap<TodoDto, TodoItem>();
    }   
}