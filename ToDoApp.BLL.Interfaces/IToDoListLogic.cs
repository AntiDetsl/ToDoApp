using ToDoApp.Entities;

namespace ToDoApp.BLL.Interfaces
{
    public interface IToDoListLogic
    {
        Task AddAsync(ToDoList list);

        Task<IEnumerable<ToDoList>> GetAllAsync();

        Task<ToDoList> UpdateAsync(Guid id, string title);

        Task DeleteAsync(Guid id);

        Task<ToDoList> GetByIdAsync(Guid id);

        Task<IEnumerable<ToDoList>> GetAllWithTaskAsync();
    }
}
