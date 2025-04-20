using Identity.Service.Infrastructure.Extensions;
using Identity.Service.Presentation.Extensions;
using TodoList.Service.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder);

var app = builder.Build();
app.ConfigureWebApplication();
app.RegisterIdentityEndpoints();

app.Run();
