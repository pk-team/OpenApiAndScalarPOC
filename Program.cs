using Scalar.AspNetCore;
using OpenApi3.Extensions;

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


    app.SetupTodoApi();

    app.Run();
}


