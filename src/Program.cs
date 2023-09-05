

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddRabbitMQ(rabbitConnectionString: "amqp://localhost:5672", tags: new string[] { "rabbitmq" });

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => "Hello World!");



app.Run();
