using Microsoft.EntityFrameworkCore;
using TodoList.Service.Domain.Entities;

namespace TodoList.Service.Infrastructure.Contexts;

public class TodoListContext : DbContext
{
  public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .Entity<User>()
      .HasNoKey()
      .ToView(null);

    modelBuilder
      .Entity<Todo>(entity =>
      {
        entity.Ignore(p => p.IsNull);
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Name).IsRequired();
        entity.Property(p => p.UserId).IsRequired();
      });

    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Todo> Todos => Set<Todo>();
}