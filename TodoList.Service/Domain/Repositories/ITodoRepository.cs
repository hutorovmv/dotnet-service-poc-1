using Identity.Service.Domain.Entities;

namespace Identity.Service.Domain.Repositories;

public interface ITodoRepository
{
  public Task<Todo> GetAsync(int id, string userId);
  public Task<IEnumerable<Todo>> GetAllAsync(string userId);
  public Task<Todo> CreateAsync(Todo todo, string userId);
  public Task<Todo> UpdateAsync(int id, Todo newTodo, string userId);
  public Task<Todo> DeleteAsync(int id, string userId);
}