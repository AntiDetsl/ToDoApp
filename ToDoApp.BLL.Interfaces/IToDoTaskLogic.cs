using ToDoApp.Entities;

namespace ToDoApp.BLL.Interfaces
{
    public interface IToDoTaskLogic
    {
        Task AddAsync(Guid listId, ToDoTask task);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ToDoTask>> GetAllAsync(Guid listId);

        Task<ToDoTask> GetByIdAsync(Guid id);

        Task<ToDoTask> ChangeCompletedStatusAsync(Guid id);

        Task<ToDoTask> ChangeImportantStatusAsync(Guid id);

        Task<ToDoTask> UpdateDueDateAsync(Guid id, DateTime? date);

        Task<ToDoTask> UpdateReminderAsync(Guid id, DateTime? reminder);

        Task<ToDoTask> UpdateTitleAsync(Guid id, string title);

        Task<IEnumerable<Tuple<ToDoList, ToDoTask>>> GetAllForToday();
    }
}
