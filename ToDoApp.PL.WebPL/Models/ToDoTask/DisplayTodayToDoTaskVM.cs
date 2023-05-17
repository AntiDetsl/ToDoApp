namespace ToDoApp.PL.WebPL.Models.ToDoTaskVM
{
    public class DisplayTodayToDoTaskVM
    {
        public string Title { get; set; } = null!;
        
        public string ListName { get; set; } = null!;

        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? Reminder { get; set; }
    }
}
