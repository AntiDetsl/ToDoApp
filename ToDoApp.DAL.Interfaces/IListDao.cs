using ToDoApp.Entities;

namespace ToDoApp.DAL.Interfaces
{
    public interface IListDao
    {
        Task AddAsync(ToDoList list);

        Task<ToDoList> UpdateAsync(Guid id, string title);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ToDoList>> GetAllAsync();

        Task<ToDoList> GetByIdAsync(Guid id);

        Task<IEnumerable<ToDoList>> GetAllWithTaskAsync();
    }
}
