using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoApp.DAL.Context;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Entities;

namespace ToDoApp.DAL
{
    public class TaskStepDao : ITaskStepDao
    {
        private readonly ToDoAppDbContext _context;
        public TaskStepDao(ToDoAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Guid taskId, ToDoTaskStep step)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }

            var task = await _context.ToDoTasks
                .Include(t => t.ToDoTaskSteps)
                .SingleOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentNullException(nameof(taskId), "Wrong ID. Task not found.");
            }

            task.ToDoTaskSteps.Add(step);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid taskId, Guid stepId)
        {
            var task = await _context.ToDoTasks
                .Include(t => t.ToDoTaskSteps)
                .SingleOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentNullException(nameof(taskId), "Wrong ID. Task not found.");
            }

            var step = task.ToDoTaskSteps.SingleOrDefault(s => s.Id == stepId);

            if (step == null)
            {
                throw new ArgumentNullException(nameof(stepId), "Wrong ID. Step not found.");
            }

            _context.Remove(step);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTaskStep>> GetAllAsync(Guid taskId)
        {
            var task = await _context.ToDoTasks
                .AsNoTracking()
                .Include(t => t.ToDoTaskSteps)
                .SingleOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentNullException(nameof(taskId), "Wrong ID. Task not found.");
            }

            return task.ToDoTaskSteps;
        }

        public async Task<ToDoTaskStep> GetByIdAsync(Guid Id)
        {
            var step = await _context.ToDoTasks
                .Include(t => t.ToDoTaskSteps)
                .SelectMany(t => t.ToDoTaskSteps)
                .SingleOrDefaultAsync(s => s.Id == Id);

            if (step == null)
            {
                throw new ArgumentNullException(nameof(Id), "Wrong ID. Step not found.");
            }

            return step;
        }

        public async Task<ToDoTaskStep> UpdateAsync(ToDoTaskStep step)
        {
            _context.Update(step);
            await _context.SaveChangesAsync();

            return step;
        }
    }
}
