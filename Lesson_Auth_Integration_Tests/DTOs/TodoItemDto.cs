namespace Lesson_Auth_Integration_Tests.DTOs;

public record TodoItemDto(int Id, string Title, bool IsCompleted, string OwnerEmail)
{
    public TodoItemDto() : this(0, "", false, "") { }
}