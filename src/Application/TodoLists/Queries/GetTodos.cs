using CleanArchitectureSample.Application.Common.Interfaces;
using CleanArchitectureSample.Application.Common.Models;
using CleanArchitectureSample.Application.Common.Security;
using CleanArchitectureSample.Domain.Enums;

namespace CleanArchitectureSample.Application.TodoLists.Queries;

[Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<GetTodosQuery, TodosVm>
{
    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),

            Lists = await context.TodoLists
                .AsNoTracking()
                .ProjectTo<TodoListDto>(mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
