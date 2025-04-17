using Microsoft.EntityFrameworkCore;
using TodoList.Service.Domain;
using TodoList.Service.Domain.Repositories;

namespace TodoList.Service.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
  private readonly TodoListContext db;

  public TodoRepository(TodoListContext db)
  {
    this.db = db;
  }

  public async Task CreateAsync(Todo todo)
  {
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
  }

  public async Task<Todo> DeleteAsync(int id)
  {
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
      db.Todos.Remove(todo);
      await db.SaveChangesAsync();
      return todo;
    }

    return Todo.NullTodo;
  }

  public async Task<IEnumerable<Todo>> GetAllAsync()
  {
    return await db.Todos.ToListAsync();
  }

  public async Task<Todo> GetAsync(int id)
  {
    return await db.Todos.FindAsync(id) is Todo todo ? todo : Todo.NullTodo;
  }
}