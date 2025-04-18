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

  public async Task<Todo> GetAsync(int id)
  {
    return await db.Todos.FindAsync(id) is Todo todo ? todo : Todo.NullTodo;
  }

  public async Task<IEnumerable<Todo>> GetAllAsync()
  {
    return await db.Todos.ToListAsync();
  }

  public async Task<Todo> CreateAsync(Todo todo)
  {
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return todo;
  }

  public async Task<Todo> UpdateAsync(int id, Todo newTodo)
  {
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
    {
      return Todo.NullTodo;
    }

    todo.Name = newTodo.Name;
    todo.IsComplete = newTodo.IsComplete;

    await db.SaveChangesAsync();
    return todo;
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
}