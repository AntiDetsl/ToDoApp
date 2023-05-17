using Microsoft.EntityFrameworkCore;
using ToDoApp.Entities;

namespace ToDoApp.DAL.Context
{
    public class ToDoAppDbContext : DbContext
    {
        public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) : base(options) {}

        public DbSet<ToDoList> ToDoLists { get; set; } = null!;
        public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
    }
}
