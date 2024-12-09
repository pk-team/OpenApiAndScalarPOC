namespace OpenApi3.Common;

public class Error {

    public Error() { }

    public Error(string description) {
        Code = "Error";
        Description = description;
    }

    public Error(string code, string description) {
        Code = code;
        Description = description;
    }

    public string Code { get; set; } = "";
    public string Description { get; set; } = "";
}

public static class ErrorExtension {
    public static void AddError(this List<Error> errors, string description) {
        errors.Add(new Error(description));
    }
}