using System.ComponentModel;
using ToDoApp.Entities;

namespace ToDoApp.PL.WebPL.Models.ToDoTaskVM
{
    public class ToDoTaskDetailsVM
    {
        public string Title { get; set; } = null!;

        [DisplayName("Goal date")]
        public DateTime? DueDate { get; set; }

        public IEnumerable<ToDoTaskStep>? steps { get; set; }
    }
}
