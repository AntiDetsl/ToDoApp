using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Context;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Entities;

namespace ToDoApp.DAL
{
    public class ListDao : IListDao
    {
        private readonly ToDoAppDbContext _context;

        public ListDao(ToDoAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ToDoList list)
        {
            _context.Add(list);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var list = await _context.ToDoLists
                .Include(l => l.TaskList)
                .ThenInclude(t => t.ToDoTaskSteps)
                .SingleOrDefaultAsync(l => l.Id == id);
            
            if(list == null)
            {
                throw new ArgumentNullException(nameof(id), "Wrong ID. List not found.");
            }

            _context.Remove(list);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            IQueryable<ToDoList> lists = _context.ToDoLists.AsNoTracking();
            return await lists.ToListAsync();
        }

        public async Task<IEnumerable<ToDoList>> GetAllWithTaskAsync()
        {
            IQueryable<ToDoList> lists = _context.ToDoLists.Include(l=>l.TaskList).AsNoTracking();
            return await lists.ToListAsync();
        }

        public async Task<ToDoList> GetByIdAsync(Guid id)
        {
            var list = await _context.ToDoLists
                .AsNoTracking()
                .SingleAsync(l=>l.Id == id);

            return list;
        }

        public async Task<ToDoList> UpdateAsync(Guid id, string title)
        {
            var list = await _context.ToDoLists.SingleOrDefaultAsync(l=>l.Id == id);
            if(list == null)
            {
                throw new ArgumentNullException(nameof(id), "Wrong ID. List not found.");
            }

            list.Name = title;
            await _context.SaveChangesAsync();

            return list;
        }
    }
}
