using FluentValidation;
using MediatR;

namespace CalzolariDemoApp.Server.Greetings;

public record HellooRequest(string FirstName, string LastName) : IRequest<HelloResponse>;

public record HelloResponse(string Message);

public class HelloRequestValidator : AbstractValidator<HellooRequest>
{
    public HelloRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .WithMessage("Give a last name with a length greater than 3");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .WithMessage("Give a last name with a length greater than 3");
    }
}

public static class HelloExtensions
{
    public static HellooRequest Create(this HelloRequest request)
        => new HellooRequest(request.FirstName, request.LastName);
}

public class HelloRequestHandler : IRequestHandler<HellooRequest, HelloResponse>
{
    public Task<HelloResponse> Handle(HellooRequest request, CancellationToken cancellationToken)
        => Task.FromResult(new HelloResponse("Hello " + request.LastName+", " + request.FirstName));
}