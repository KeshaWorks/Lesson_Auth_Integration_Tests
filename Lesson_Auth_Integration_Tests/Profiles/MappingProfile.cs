<<<<<<< HEAD
﻿using AutoMapper;
=======
﻿// ====================================================================================================
// Profiles/MappingProfile.cs
// ====================================================================================================
using AutoMapper;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Lesson_Auth_Integration_Tests.DTOs;
using Lesson_Auth_Integration_Tests.Models;
using Microsoft.AspNetCore.Identity;

namespace Lesson_Auth_Integration_Tests.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
    }
}