using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entities;

namespace ToDoApp.DAL.Interfaces
{
    public interface ITaskStepDao
    {
        Task AddAsync(Guid taskId, ToDoTaskStep step);

        Task DeleteAsync(Guid taskId, Guid stepId);

        Task<IEnumerable<ToDoTaskStep>> GetAllAsync(Guid taskId);

        Task<ToDoTaskStep> UpdateAsync(ToDoTaskStep step);

        Task<ToDoTaskStep> GetByIdAsync(Guid Id);
    }
}
