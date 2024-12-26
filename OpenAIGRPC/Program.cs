using Application.Interfaces;
using Application.UseCases;
using Domain.Interfaces.Services;
using Infrastructure.Implementations;
using OpenAIGRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IAiService, OpenAiService>();
builder.Services.AddScoped<IRequest, HTTPService>();
builder.Services.AddScoped<GenerateAnswerUseCase>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GenerateService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();