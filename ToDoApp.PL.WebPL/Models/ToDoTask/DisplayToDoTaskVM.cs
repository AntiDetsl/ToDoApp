using System.ComponentModel;

namespace ToDoApp.PL.WebPL.Models.ToDoTaskVM
{
    public class DisplayToDoTaskVM
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DisplayName("Goal date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Important")]
        public bool IsImportant { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; } = false;

        [DisplayName("Reminder")]
        public DateTime? TaskReminderDateTime { get; set; }
    }
}
