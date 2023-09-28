using CleanArchitectureSample.Application.Common.Interfaces;
using CleanArchitectureSample.Application.Common.Security;
using CleanArchitectureSample.Domain.Constants;

namespace CleanArchitectureSample.Application.TodoLists.Commands;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanPurge)]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler(IApplicationDbContext context) : IRequestHandler<PurgeTodoListsCommand>
{
    public async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        context.TodoLists.RemoveRange(context.TodoLists);

        await context.SaveChangesAsync(cancellationToken);
    }
}
