using ToDoApp.Entities;

namespace ToDoApp.DAL.Interfaces
{
    public interface IToDoTaskDao
    {
        Task AddAsync(Guid listId, ToDoTask task);

        Task<ToDoTask> UpdateAsync(ToDoTask task);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ToDoTask>> GetAllAsync(Guid listId);

        Task<ToDoTask> GetByIdAsync(Guid id);
        Task<IEnumerable<Tuple<ToDoList, ToDoTask>>> GetAllForToday();
    }
}
