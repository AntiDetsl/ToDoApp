using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Entities;

namespace ToDoApp.BLL
{
    public class ToDoTaskLogic : IToDoTaskLogic
    {
        private readonly IToDoTaskDao _taskDao;

        public ToDoTaskLogic(IToDoTaskDao taskDao) 
        {
            _taskDao = taskDao;
        }

        public async Task AddAsync(Guid listId, ToDoTask task)
        {
            await _taskDao.AddAsync(listId, task);
        }

        public async Task<ToDoTask> ChangeCompletedStatusAsync(Guid id)
        {
            var task = await _taskDao.GetByIdAsync(id);
            task.IsCompleted = !task.IsCompleted;

            if(task.IsCompleted)
            {
                task.DueDate = null;
                task.TaskReminderDateTime = null;
            }

            return await _taskDao.UpdateAsync(task);
        }

        public async Task<ToDoTask> ChangeImportantStatusAsync(Guid id)
        {
            var task = await _taskDao.GetByIdAsync(id);
            task.IsImportant = !task.IsImportant;
            return await _taskDao.UpdateAsync(task);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _taskDao.DeleteAsync(id);
        }

        public async Task<IEnumerable<ToDoTask>> GetAllAsync(Guid listId)
        {
            return await _taskDao.GetAllAsync(listId);
        }

        public async Task<IEnumerable<Tuple<ToDoList, ToDoTask>>> GetAllForToday()
        {
            return await _taskDao.GetAllForToday();
        }

        public async Task<ToDoTask> GetByIdAsync(Guid id)
        {
            return await _taskDao.GetByIdAsync(id);
        }

        public async Task<ToDoTask> UpdateDueDateAsync(Guid id, DateTime? date)
        {
            var task = await _taskDao.GetByIdAsync(id);
            task.DueDate = date;
            return await _taskDao.UpdateAsync(task);
        }

        public async Task<ToDoTask> UpdateReminderAsync(Guid id, DateTime? reminder)
        {
            var task = await _taskDao.GetByIdAsync(id);
            task.TaskReminderDateTime = reminder;
            return await _taskDao.UpdateAsync(task);
        }

        public async Task<ToDoTask> UpdateTitleAsync(Guid id, string title)
        {
            var task = await _taskDao.GetByIdAsync(id);
            task.Title = title;
            return await _taskDao.UpdateAsync(task);
        }
    }
}
