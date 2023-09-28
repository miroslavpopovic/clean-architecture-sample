using CleanArchitectureSample.Application.Common.Interfaces;
using CleanArchitectureSample.Domain.Entities;

namespace CleanArchitectureSample.Application.TodoLists.Commands;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken) =>
        await _context.TodoLists
            .AllAsync(l => l.Title != title, cancellationToken);
}

public class CreateTodoListCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTodoListCommand, int>
{
    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList { Title = request.Title };

        context.TodoLists.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
