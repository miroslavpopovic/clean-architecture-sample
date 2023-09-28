using CleanArchitectureSample.Application.Common.Interfaces;

namespace CleanArchitectureSample.Application.TodoLists.Commands;

public record DeleteTodoListCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTodoListCommand>
{
    public async Task Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        context.TodoLists.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
