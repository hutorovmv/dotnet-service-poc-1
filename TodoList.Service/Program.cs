using TodoList.Service.Infrastructure.Extensions;
using TodoList.Service.Presentation.Extensions;
using TodoList.Service.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder);

var app = ConfigureKestrel(builder).Build();
app.ConfigureWebApplication();
app.RegisterTodoCrudEndpoints();

app.Run();

WebApplicationBuilder ConfigureKestrel(WebApplicationBuilder builder)
{
  builder.WebHost.ConfigureKestrel(options =>
  {
    options.ListenAnyIP(8080);
    options.ListenAnyIP(8443, listenOptions =>
    {
      listenOptions.UseHttps(
        builder.Configuration["ASPNETCORE:Kestrel:Certificates:Default:Path"] ?? "",
        builder.Configuration["ASPNETCORE:Kestrel:Certificates:Default:Password"] ?? ""
      );
    });
  });

  return builder;
}
