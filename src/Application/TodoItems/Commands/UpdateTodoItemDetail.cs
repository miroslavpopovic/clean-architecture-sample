using CleanArchitectureSample.Application.Common.Interfaces;
using CleanArchitectureSample.Domain.Enums;

namespace CleanArchitectureSample.Application.TodoItems.Commands;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<UpdateTodoItemDetailCommand>
{
    public async Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await context.SaveChangesAsync(cancellationToken);
    }
}
