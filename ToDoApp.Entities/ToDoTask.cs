namespace ToDoApp.Entities
{
    public class ToDoTask
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        public bool IsImportant { get; set; } = false;

        public bool IsCompleted { get; set; } = false;

        public DateTime? TaskReminderDateTime { get; set; }

        public Guid ToDoListId { get; set; }

        public ToDoList? ToDoList { get; set; }

        public List<ToDoTaskStep>? ToDoTaskSteps { get; set;}
    }
}
