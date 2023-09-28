using Asp.Versioning.Builder;
using CleanArchitectureSample.Application.TodoLists.Commands;
using CleanArchitectureSample.Application.TodoLists.Queries;

namespace CleanArchitectureSample.Web.Endpoints;

public static class TodoLists
{
    public static IEndpointRouteBuilder MapTodoLists(this IEndpointRouteBuilder app, ApiVersionSet versionSet)
    {
        var group = app
            .MapGroup("/api/v{version:apiVersion}/todo-lists")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization()
            .WithTags(nameof(TodoLists));
        
        group
            .MapGet("/", GetTodoLists)
            .WithName(nameof(GetTodoLists))
            .WithOpenApi(
                "Get a list of todo items.",
                "Get all available todo items.");
        
        group
            .MapPost("/", CreateTodoList)
            .WithName(nameof(CreateTodoList))
            .WithOpenApi(
                "Create a new todo list.",
                "Creates a new todo list with supplied values.");

        group
            .MapPut("{id}", UpdateTodoList)
            .WithName(nameof(UpdateTodoList))
            .WithOpenApi(
                "Update a todo list by id.",
                "Updates a todo list with the given id, using the supplied data.",
                "Id of the client to update.");

        group
            .MapDelete("{id}", DeleteTodoList)
            .WithName(nameof(DeleteTodoList))
            .WithOpenApi(
                "Delete a todo list by id.",
                "Deletes a single todo list with the given id.",
                "Id of the client to delete.");

        return app;
    }

    private static async Task<TodosVm> GetTodoLists(ISender sender) => await sender.Send(new GetTodosQuery());

    private static async Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command) =>
        await sender.Send(command);

    private static async Task<IResult> UpdateTodoList(ISender sender, int id, UpdateTodoListCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        
        await sender.Send(command);
        
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTodoList(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoListCommand(id));
        
        return Results.NoContent();
    }
}
