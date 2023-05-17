using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Entities;

namespace ToDoApp.BLL
{
    public class ToDoListLogic : IToDoListLogic
    {
        private readonly IListDao _listDao;
        public ToDoListLogic(IListDao listDao) 
        {
            _listDao = listDao;
        }
        public async Task AddAsync(ToDoList list)
        {
            if(list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            await _listDao.AddAsync(list);
        }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            return await _listDao.GetAllAsync();
        }

        public async Task<ToDoList> UpdateAsync(Guid id, string title)
        {
            return await _listDao.UpdateAsync(id, title);
        }

        public async Task DeleteAsync(Guid id)
        {
             await _listDao.DeleteAsync(id);
        }

        public async Task<ToDoList> GetByIdAsync(Guid id)
        {
            return await _listDao.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ToDoList>> GetAllWithTaskAsync()
        {
            return await _listDao.GetAllWithTaskAsync();
        }
    }
}
