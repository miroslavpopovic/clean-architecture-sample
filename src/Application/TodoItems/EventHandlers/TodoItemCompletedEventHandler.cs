using CleanArchitectureSample.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureSample.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger) : INotificationHandler<TodoItemCompletedEvent>
{
    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("CleanArchitectureSample Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
