namespace TodoList.Service.Domain.Repositories;

public interface ITodoRepository
{
  public Task<Todo> GetAsync(int id);
  public Task<IEnumerable<Todo>> GetAllAsync();
  public Task CreateAsync(Todo todo);
  public Task<Todo> DeleteAsync(int id);
}