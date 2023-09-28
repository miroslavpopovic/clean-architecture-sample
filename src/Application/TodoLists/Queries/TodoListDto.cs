using CleanArchitectureSample.Domain.Entities;

namespace CleanArchitectureSample.Application.TodoLists.Queries;

public class TodoListDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Colour { get; init; }

    public IReadOnlyCollection<TodoItemDto> Items { get; init; } = Array.Empty<TodoItemDto>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TodoList, TodoListDto>();
        }
    }
}
