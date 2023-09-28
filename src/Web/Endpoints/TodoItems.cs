using Asp.Versioning.Builder;
using CleanArchitectureSample.Application.Common.Models;
using CleanArchitectureSample.Application.TodoItems.Commands;
using CleanArchitectureSample.Application.TodoItems.Queries;

namespace CleanArchitectureSample.Web.Endpoints;

public static class TodoItems
{
    public static IEndpointRouteBuilder MapTodoItems(this IEndpointRouteBuilder app, ApiVersionSet versionSet)
    {
        var group = app
            .MapGroup("/api/v{version:apiVersion}/todo-items")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization()
            .WithTags(nameof(TodoItems));

        group
            .MapGet("/", GetTodoItemsWithPagination)
            .WithName(nameof(GetTodoItemsWithPagination))
            .WithOpenApi(
                "Get a paginated list of todo items.",
                "Get one page of todo items.");

        group
            .MapPost("/", CreateTodoItem)
            .WithName(nameof(CreateTodoItem))
            .WithOpenApi(
                "Create a new todo item.",
                "Creates a new todo item with supplied values.");
        
        group
            .MapPut("{id}", UpdateTodoItem)
            .WithName(nameof(UpdateTodoItem))
            .WithOpenApi(
                "Update a todo item by id.",
                "Updates a todo item with the given id, using the supplied data.",
                "Id of the todo item to update.");
        
        group
            .MapPut("detail/{id}", UpdateTodoItemDetail)
            .WithName(nameof(UpdateTodoItemDetail))
            .WithOpenApi(
                "Update a todo item detail by id.",
                "Updates a todo item detail with the given id, using the supplied data.",
                "Id of the todo item detail to update.");
        
        group
            .MapDelete("{id}", DeleteTodoItem)
            .WithName(nameof(DeleteTodoItem))
            .WithOpenApi(
                "Delete a todo item by id.",
                "Deletes a single todo item with the given id.",
                "Id of the todo item to delete.");

        return app;
    }

    private static async Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(
        ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query) => 
        await sender.Send(query);

    private static async Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command) => 
        await sender.Send(command);

    private static async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        
        await sender.Send(command);
        
        return Results.NoContent();
    }

    private static async Task<IResult> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        
        await sender.Send(command);
        
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTodoItem(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoItemCommand(id));
        
        return Results.NoContent();
    }
}
