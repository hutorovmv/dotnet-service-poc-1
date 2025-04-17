namespace TodoList.Service.Presentation;

public static class WebApplicationExtensions
{
  public static void ConfigureWebApplication(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseOpenApi();
      app.UseSwaggerUi(config =>
      {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
      });
    }
  }
}