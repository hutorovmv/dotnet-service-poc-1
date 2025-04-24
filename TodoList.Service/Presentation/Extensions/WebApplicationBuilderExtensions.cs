namespace TodoList.Service.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsDevelopment())
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
    }

    return builder;
  }
}