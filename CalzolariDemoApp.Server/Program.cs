using Calzolari.Grpc.AspNetCore.Validation;
using CalzolariDemoApp.Server;
using CalzolariDemoApp.Server.Greetings;
using CalzolariDemoApp.Server.Services;
using FluentValidation;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((ctx, opt) =>
{
    opt.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        listenOptions.UseHttps();
    });
});
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(opt => opt.EnableMessageValidation());

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddGrpcValidation();

// Second approach to validate
// builder.Services.AddInlineValidator<HelloRequest>(rules =>
// {
//     rules.RuleFor(request => request.FirstName).NotEmpty().MinimumLength(3);
//     rules.RuleFor(request => request.LastName).NotEmpty().MinimumLength(3);
// });

// First approach to validate
builder.Services.AddValidator<HelloRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();