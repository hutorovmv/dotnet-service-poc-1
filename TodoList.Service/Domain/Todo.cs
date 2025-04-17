using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoList.Service.Domain;

public class Todo
{
  public int Id { get; set;}
  public string? Name { get; set; }
  public bool IsComplete { get; set; }

  [JsonIgnore]
  public bool IsNull { get; private set; } = false;

  [JsonIgnore]
  public static Todo NullTodo => new()
  {
    Id = -1,
    Name = null,
    IsComplete = false,
    IsNull = true
  };
}