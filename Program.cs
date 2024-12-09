using App;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args); {

    builder.Services.AddOpenApi();
    builder.Services.AddSingleton(TimeProvider.System);
    builder.Services.AddEndpointsApiExplorer();

}

var app = builder.Build(); {

    if (app.Environment.IsDevelopment()) {
        app.MapOpenApi(); // registers the endpoint that registers the json document
        app.MapScalarApiReference();
    }


    var api = app.MapGroup("api");

    var todos = new List<Todo>
    {
        new() { Id = 1, Title = "Learn .NET 9", Completed = false },
        new() { Id = 2, Title = "Build Minimal API", Completed = true },
        new() { Id = 3, Title = "Explore OpenAPI", Completed = false }
    };

    api.Map("/v1", () => "Todos API");

    api.MapGet("/v1/todos/{id}", (int id) =>
        todos.FirstOrDefault(x => x.Id == id));

    api.MapPost("/v1/create-todo", (CreateTodoInput input) => {
        var todo = new Todo {
            Id = todos.Count + 1,
            Title = input.Title,
            Completed = false
        };
        todos.Add(todo);
        return todo;
    });


    api.MapPost("/v1/update-todo", (UpdateTodoInput updateInput) => {
        var index = todos.FindIndex(x => x.Id == updateInput.Id);
        if (index != -1) {
            todos[index].Title = updateInput.Title;
            todos[index].Completed = updateInput.Completed;
            return todos[index];
        }
        return null;
    });

    api.MapGet("/v1/todos", () => todos);




    app.Run();
}


namespace App {
    public class Todo {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }

    public class CreateTodoInput {
        public string Title { get; set; } = string.Empty;
    }
    public class UpdateTodoInput {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}