using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Context;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Entities;

namespace ToDoApp.DAL
{
    public class ToDoTaskDao : IToDoTaskDao
    {
        private readonly ToDoAppDbContext _context;

        public ToDoTaskDao(ToDoAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Guid listId, ToDoTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var list = await _context.ToDoLists
                .Include(l => l.TaskList)
                .SingleOrDefaultAsync(l => l.Id == listId);

            if(list == null) 
            {
                throw new ArgumentNullException(nameof(listId), "Wrong ID. List not found.");
            }

            list.TaskList.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.ToDoTasks
                .Include(t => t.ToDoTaskSteps)
                .SingleOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new ArgumentNullException(nameof(id), "Wrong Id. Task not found.");
            }

            _context.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTask>> GetAllAsync(Guid listId)
        {
            var tasks =  await _context.ToDoTasks
                .AsNoTracking()
                .Where(t=>t.ToDoListId == listId)
                .ToListAsync();

            return tasks;
        }

        public async Task<ToDoTask> GetByIdAsync(Guid id)
        {
            var task = await _context.ToDoTasks.SingleOrDefaultAsync(t => t.Id == id);
            
            if (task == null)
            {
                throw new ArgumentNullException(nameof(id), "Wrong Id. Task not found.");
            }
            return task;
        }

        public async Task<ToDoTask> UpdateAsync(ToDoTask task)
        {
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<IEnumerable<Tuple<ToDoList, ToDoTask>>> GetAllForToday()
        {
            var tasks = await _context.ToDoLists.AsNoTracking()
                .Join(
                    _context.ToDoTasks,
                    list => list.Id,
                    task => task.ToDoListId,
                    (list, task) => new { list, task }
                )
                .Where(i=>i.task.DueDate.HasValue && 
                    i.task.DueDate.Value.Date == DateTime.Now.Date)
                .ToListAsync();

            return tasks.Select(t=> new Tuple<ToDoList, ToDoTask>(t.list, t.task));
        }
    }
}
