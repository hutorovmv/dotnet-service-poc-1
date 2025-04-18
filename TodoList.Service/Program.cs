using TodoList.Service.Infrastructure;
using TodoList.Service.Presentation;
using TodoList.Service.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder);

var app = builder.Build();
app.ConfigureWebApplication();
app.RegisterTodoCrudEndpoints();

app.Run();
