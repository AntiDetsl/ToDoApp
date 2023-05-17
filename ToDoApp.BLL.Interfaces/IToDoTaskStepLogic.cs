using ToDoApp.Entities;

namespace ToDoApp.BLL.Interfaces
{
    public interface IToDoTaskStepLogic
    {
        Task AddAsync(Guid taskId, string stepDesc);

        Task DeleteAsync(Guid taskId, Guid stepId);

        Task<IEnumerable<ToDoTaskStep>> GetAllAsync(Guid taskId);

        Task<ToDoTaskStep> UpdateDescAsync(Guid Id, string desc);

        Task<ToDoTaskStep> GetByIdAsync(Guid Id);

        Task ChangeCompletedStatusAsync(Guid id);
    }
}
