namespace OpenApi3.Models;

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
