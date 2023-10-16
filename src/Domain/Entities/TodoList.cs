namespace CleanArchitectureSample.Domain.Entities;

public class TodoList : BaseEntity, IAuditable
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
