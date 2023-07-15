using Grpc.Core;
using CalzolariDemoApp.Server;
using CalzolariDemoApp.Server.Greetings;
using MediatR;

namespace CalzolariDemoApp.Server.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IMediator _mediator;

    public GreeterService(ILogger<GreeterService> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var response = await _mediator.Send(request.Create());
        return new HelloReply() { Message = response.Message };
    }
}