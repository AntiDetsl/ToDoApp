using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Entities
{
    public class ToDoList
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ToDoTask> TaskList { get; set; }
    }
}
