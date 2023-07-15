// IoC way to use gRPC clients.

using Calzolari.Grpc.Net.Client.Validation;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using Microsoft.Extensions.DependencyInjection;

var httpHandler = new HttpClientHandler();
httpHandler.ServerCertificateCustomValidationCallback
    = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

var httpClient = new HttpClient(httpHandler);
using var channel = GrpcChannel.ForAddress(
    "https://localhost:5001", 
    new GrpcChannelOptions { HttpClient = httpClient }
    );


var greeterClient = new Greeter.GreeterClient(channel);
var greetRequest = new HelloRequest(){FirstName ="Se", LastName = "ed"}; 

try
{
    Console.WriteLine(greetRequest);
    var greetResponse = await greeterClient.SayHelloAsync(greetRequest);
    Console.WriteLine(greetResponse);
}
catch (RpcException e)
{
    var errors = e.GetValidationErrors();
    Console.WriteLine(e.Message);
}