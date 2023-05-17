using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Entities;

namespace ToDoApp.BLL
{
    public class ToDoTaskStepLogic : IToDoTaskStepLogic
    {
        private readonly ITaskStepDao _stepDao;

        public ToDoTaskStepLogic(ITaskStepDao stepDao)
        {
            _stepDao = stepDao;
        }

        public async Task AddAsync(Guid taskId, string stepDesc)
        {
            var step = new ToDoTaskStep();
            step.Description = stepDesc;

            await _stepDao.AddAsync(taskId, step);
        }

        public async Task ChangeCompletedStatusAsync(Guid id)
        {
            var step = await _stepDao.GetByIdAsync(id);
            step.IsCompleted = !step.IsCompleted;
            await _stepDao.UpdateAsync(step);
        }

        public async Task DeleteAsync(Guid taskId, Guid stepId)
        {
            await _stepDao.DeleteAsync(taskId, stepId);
        }

        public async Task<IEnumerable<ToDoTaskStep>> GetAllAsync(Guid taskId)
        {
            return await _stepDao.GetAllAsync(taskId);
        }

        public async Task<ToDoTaskStep> GetByIdAsync(Guid Id)
        {
            return await _stepDao.GetByIdAsync(Id);
        }

        public async Task<ToDoTaskStep> UpdateDescAsync(Guid Id, string desc)
        {
            var task = await _stepDao.GetByIdAsync(Id);
            task.Description = desc;
            return await _stepDao.UpdateAsync(task);
        }
    }
}
