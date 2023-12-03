using DFP.SERVICE;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
