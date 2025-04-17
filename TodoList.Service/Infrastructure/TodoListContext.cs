using Microsoft.EntityFrameworkCore;
using TodoList.Service.Domain;

namespace TodoList.Service.Infrastructure;

public class TodoListContext : DbContext
{
  public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Todo>().HasKey(p => p.Id);
    modelBuilder.Entity<Todo>().Ignore(p => p.IsNull);
  }

  public DbSet<Todo> Todos => Set<Todo>();
}