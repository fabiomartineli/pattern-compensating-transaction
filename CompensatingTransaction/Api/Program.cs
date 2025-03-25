using Api.HostServics;
using Application;
using Application.Settings;
using Infra.Data;
using Infra.Data.Settings;
using Infra.MessageBus;
using Infra.MessageBus.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(x => x.AddPolicy("localhost", policy =>
{
    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
}));
builder.Services.Configure<DatabaseContextSettings>(builder.Configuration.GetSection(nameof(DatabaseContextSettings)));
builder.Services.Configure<MessageBusClientSettings>(builder.Configuration.GetSection(nameof(MessageBusClientSettings)));
builder.Services.Configure<MessageBusDestinationSettings>(builder.Configuration.GetSection(nameof(MessageBusDestinationSettings)));
builder.Services.AddDatabaseIoC(builder.Configuration);
builder.Services.AddApplicationIoC(builder.Configuration);
builder.Services.AddMessageBusIoC(builder.Configuration);
builder.Services.AddMessageBusConsumers(builder.Configuration);

var app = builder.Build();
app.MapOpenApi();
app.UseCors("localhost");
app.UseAuthorization();
app.MapControllers();
app.Run();
