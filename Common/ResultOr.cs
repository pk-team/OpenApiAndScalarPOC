namespace OpenApi3.Common;

public class ResultOr<T> where T : class {
    public T? Payload { get; set; }
    public List<Error> Errors { get; set; } = [];

    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => Errors.Count != 0;
    public string ErrorMessage => string.Join(", ", Errors.Select(e => e.Description));

    public ResultOr(T? payload = null) {
        Payload = payload;
    }


    public ResultOr(string error) {
        Errors = [new Error(error)];
    }
    public ResultOr(Error error) {
        Errors = [error];
    }
    public ResultOr(IEnumerable<string> errors) {
        Errors = errors.Select(e => new Error(e)).ToList();
    }
    public ResultOr(IEnumerable<Error> errors) {
        Errors = errors.ToList();
    }

    public static ResultOr<T> Success(T value) => new(value);
    public static ResultOr<T> Failure(string error) => new(error);
    public static ResultOr<T> Failure(Error error) => new(error);
    public static ResultOr<T> Failure(IEnumerable<string> errors) => new(errors);
    public static ResultOr<T> Failure(IEnumerable<Error> errors) => new(errors);

}

