
using AutoMapper;
using Lesson_Auth_Integration_Tests.DTOs;
using Lesson_Auth_Integration_Tests.Models;

namespace Lesson_Auth_Integration_Tests.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
    }
}