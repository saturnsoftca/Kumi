using Kumi.API.Application.Chats;
using Kumi.API.Application.Tools;
using Kumi.API.Application.Tools.Mappings;
using Kumi.Core;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddKumiRuntime();

builder.Services.AddScoped<ParameterMapper>();
builder.Services.AddScoped<ToolMapper>();
builder.Services.AddScoped<ToolService>();
builder.Services.AddScoped<ChatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
