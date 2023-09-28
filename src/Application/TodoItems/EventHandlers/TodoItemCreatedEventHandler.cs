using CleanArchitectureSample.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureSample.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger) 
    : INotificationHandler<TodoItemCreatedEvent>
{
    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("CleanArchitectureSample Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
