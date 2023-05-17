using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.PL.WebPL.Models.ToDoTaskVM
{
    public class CreateToDoTaskVM
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [UIHint("date")]
        [DisplayName("Goal date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Reminder")]
        public DateTime? TaskReminderDateTime { get; set; }
    }
}
