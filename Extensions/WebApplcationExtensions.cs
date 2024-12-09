using OpenApi3.Common;
using OpenApi3.Models;

namespace OpenApi3.Extensions;

public static class WebApplicationExtensions {

    public static void SetupTodoApi(this WebApplication app) {

        var api = app.MapGroup("api");

        var todos = new List<Todo>
        {
            new() { Id = 1, Title = "Learn .NET 9", Completed = false },
            new() { Id = 2, Title = "Build Minimal API", Completed = true },
            new() { Id = 3, Title = "Explore OpenAPI", Completed = false }
        };

        api.Map("/v1", () => "Todos API");

        api.MapGet("/v1/todos/{id}", (int id) => {
            var todo = todos.FirstOrDefault(x => x.Id == id);
            if (todo == null) {
                return ResultOr<Todo>.Failure("Todo not found");
            }
            return ResultOr<Todo>.Success(todo);
        });


        api.MapPost("/v1/create-todo", (CreateTodoInput input) => {
            if (string.IsNullOrWhiteSpace(input.Title)) {
                return ResultOr<Todo>.Failure("Title is required");
            }

            var todo = new Todo {
                Id = todos.Count + 1,
                Title = input.Title,
                Completed = false
            };
            todos.Add(todo);
            return ResultOr<Todo>.Success(todo);
        });


        api.MapPost("/v1/update-todo", (UpdateTodoInput updateInput) => {
            var index = todos.FindIndex(x => x.Id == updateInput.Id);
            if (index == -1) {
                return ResultOr<Todo>.Failure($"Todo not found for id {updateInput.Id}");
            }

            todos[index].Title = updateInput.Title;
            todos[index].Completed = updateInput.Completed;
            return ResultOr<Todo>.Success(todos[index]);
        });

        api.MapGet("/v1/todos", () => todos);
    }
}