using TodoList.Service.Infrastructure.Extensions;
using TodoList.Service.Presentation.Extensions;
using TodoList.Service.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterAspNetServices(builder);

var app = builder
  .ConfigureBuilder()
  .Build();
app.ConfigureWebApplication();
app.RegisterTodoCrudEndpoints();

app.Run();
