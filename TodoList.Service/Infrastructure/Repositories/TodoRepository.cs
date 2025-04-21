using Microsoft.EntityFrameworkCore;
using TodoList.Service.Domain.Entities;
using TodoList.Service.Domain.Repositories;
using TodoList.Service.Infrastructure.Contexts;

namespace TodoList.Service.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
  private readonly TodoListContext db;

  public TodoRepository(TodoListContext db)
  {
    this.db = db;
  }

  public async Task<Todo> GetAsync(int id, string userId)
  {
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
      return todo.UserId == userId ? todo : Todo.NullTodo;
    }

    return Todo.NullTodo;
  }

  public async Task<IEnumerable<Todo>> GetAllAsync(string userId)
  {
    return await db.Todos.Where(t => t.UserId == userId).ToListAsync();
  }

  public async Task<Todo> CreateAsync(Todo todo, string userId)
  {
    todo.UserId = userId;

    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return todo;
  }

  public async Task<Todo> UpdateAsync(int id, Todo newTodo, string userId)
  {
    var todo = await db.Todos.FindAsync(id);
    if (todo is null || todo.UserId != userId)
    {
      return Todo.NullTodo;
    }

    todo.Name = newTodo.Name;
    todo.IsComplete = newTodo.IsComplete;

    await db.SaveChangesAsync();
    return todo;
  }

  public async Task<Todo> DeleteAsync(int id, string userId)
  {
    if (await db.Todos.FindAsync(id) is Todo todo && todo.UserId == userId)
    {
      db.Todos.Remove(todo);
      await db.SaveChangesAsync();
      return todo;
    }

    return Todo.NullTodo;
  }
}