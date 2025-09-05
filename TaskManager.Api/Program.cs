using Serilog;
using TaskManager.Api;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("TaskManager API starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, LoggerConfiguration) => LoggerConfiguration
.WriteTo.Console()
.ReadFrom.Configuration(context.Configuration)
.ReadFrom.Services(services)
.Enrich.FromLogContext(),
true
);

var app = builder
    .ConfigureService()
    .ConfigurePipeline();

app.Run();