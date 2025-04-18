namespace TodoList.Service.Domain.Repositories;

public interface ITodoRepository
{
  public Task<Todo> GetAsync(int id);
  public Task<IEnumerable<Todo>> GetAllAsync();
  public Task<Todo> CreateAsync(Todo todo);
  public Task<Todo> UpdateAsync(int id, Todo newTodo);
  public Task<Todo> DeleteAsync(int id);
}